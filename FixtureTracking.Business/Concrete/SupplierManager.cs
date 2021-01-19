using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.SupplierValidations;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Aspects.Autofac.Performance;
using FixtureTracking.Core.Aspects.Autofac.Validation;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class SupplierManager : ISupplierService
    {
        private readonly ISupplierDal supplierDal;

        public SupplierManager(ISupplierDal supplierDal)
        {
            this.supplierDal = supplierDal;
        }

        [SecuredOperationAspect("Supplier.Add")]
        [ValidationAspect(typeof(SupplierForAddValidator))]
        [CacheRemoveAspect("ISupplierService.Get")]
        public IDataResult<int> Add(SupplierForAddDto supplierForAddDto)
        {
            var supplier = new Supplier()
            {
                CreatedAt = DateTime.Now,
                Description = supplierForAddDto.Description,
                IsEnable = true,
                Name = supplierForAddDto.Name,
                UpdatedAt = DateTime.Now
            };
            supplierDal.Add(supplier);
            return new SuccessDataResult<int>(supplier.Id, Messages.SupplierAdded);
        }

        [SecuredOperationAspect("Supplier.Delete")]
        [CacheRemoveAspect("ISupplierService.Get")]
        public IResult Delete(int supplierId)
        {
            var supplier = GetById(supplierId).Data;
            supplier.IsEnable = false;
            supplier.UpdatedAt = DateTime.Now;

            supplierDal.Update(supplier);
            return new SuccessResult(Messages.SupplierDeleted);
        }

        [SecuredOperationAspect("Supplier.Get")]
        [CacheAspect()]
        public IDataResult<Supplier> GetById(int supplierId)
        {
            var supplier = supplierDal.Get(s => s.Id == supplierId);
            if (supplier != null)
                return new SuccessDataResult<Supplier>(supplier);
            throw new ObjectNotFoundException(Messages.SupplierNotFound);
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Supplier.GetFixtures")]
        [CacheAspect(duration: 1)]
        public IDataResult<List<Fixture>> GetFixtures(int supplierId)
        {
            var supplier = GetById(supplierId).Data;
            return new SuccessDataResult<List<Fixture>>(supplierDal.GetFixtures(supplier));
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Supplier.List")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Supplier>> GetList()
        {
            return new SuccessDataResult<List<Supplier>>(supplierDal.GetList(s => s.IsEnable == true).ToList());
        }

        [SecuredOperationAspect("Supplier.Update")]
        [ValidationAspect(typeof(SupplierForUpdateValidator))]
        [CacheRemoveAspect("ISupplierService.Get")]
        public IResult Update(SupplierForUpdateDto supplierForUpdateDto)
        {
            var supplier = GetById(supplierForUpdateDto.Id).Data;
            supplier.Description = supplierForUpdateDto.Description;
            supplier.Name = supplierForUpdateDto.Name;
            supplier.UpdatedAt = DateTime.Now;

            supplierDal.Update(supplier);
            return new SuccessResult(Messages.SupplierUpdated);
        }
    }
}
