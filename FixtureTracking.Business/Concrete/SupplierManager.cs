using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.SupplierValidations;
using FixtureTracking.Core.Aspects.Autofac.Validation;
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

        [ValidationAspect(typeof(SupplierForAddValidator), Priority = 1)]
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

        public IResult Delete(int supplierId)
        {
            var supplier = GetById(supplierId).Data;
            if (supplier != null)
            {
                supplier.IsEnable = false;
                supplier.UpdatedAt = DateTime.Now;

                supplierDal.Update(supplier);
                return new SuccessResult(Messages.SupplierDeleted);
            }
            return new ErrorResult(Messages.SupplierNotFound);
        }

        public IDataResult<Supplier> GetById(int supplierId)
        {
            return new SuccessDataResult<Supplier>(supplierDal.Get(s => s.Id == supplierId));
        }

        public IDataResult<List<Fixture>> GetFixtures(int supplierId)
        {
            var supplier = GetById(supplierId).Data;
            if (supplier != null)
                return new SuccessDataResult<List<Fixture>>(supplierDal.GetFixtures(supplier));
            return new ErrorDataResult<List<Fixture>>(Messages.SupplierNotFound);
        }

        public IDataResult<List<Supplier>> GetList()
        {
            return new SuccessDataResult<List<Supplier>>(supplierDal.GetList(s => s.IsEnable == true).ToList());
        }

        [ValidationAspect(typeof(SupplierForUpdateValidator), Priority = 1)]
        public IResult Update(SupplierForUpdateDto supplierForUpdateDto)
        {
            var supplier = GetById(supplierForUpdateDto.Id).Data;
            if (supplier != null)
            {
                supplier.Description = supplierForUpdateDto.Description;
                supplier.Name = supplierForUpdateDto.Name;
                supplier.UpdatedAt = DateTime.Now;

                supplierDal.Update(supplier);
                return new SuccessResult(Messages.SupplierUpdated);
            }
            return new ErrorResult(Messages.SupplierNotFound);
        }
    }
}
