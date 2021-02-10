using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.FixtureValidations;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Aspects.Autofac.Performance;
using FixtureTracking.Core.Aspects.Autofac.Validation;
using FixtureTracking.Core.CrossCuttingConcerns.Logging.NLog.Loggers;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FixtureTracking.Business.Concrete
{
    public class FixtureManager : IFixtureService
    {
        private readonly IFixtureDal fixtureDal;

        public FixtureManager(IFixtureDal fixtureDal)
        {
            this.fixtureDal = fixtureDal;
        }

        [SecuredOperationAspect("Fixture.Add")]
        [ValidationAspect(typeof(FixtureForAddValidator))]
        [CacheRemoveAspect("IFixtureService.Get")]
        [CacheRemoveAspect("ICategoryService.GetFixtures")]
        [CacheRemoveAspect("ISupplierService.GetFixtures")]
        public IDataResult<Guid> Add(FixtureForAddDto fixtureForAddDto)
        {
            var fixture = new Fixture()
            {
                CategoryId = fixtureForAddDto.CategoryId,
                CreatedAt = DateTime.Now,
                DatePurchase = fixtureForAddDto.DatePurchase,
                DateWarranty = fixtureForAddDto.DateWarranty,
                Description = fixtureForAddDto.Description,
                FixturePositionId = 1,
                Name = fixtureForAddDto.Name,
                PictureUrl = fixtureForAddDto.PictureUrl,
                Price = fixtureForAddDto.Price,
                SupplierId = fixtureForAddDto.SupplierId,
                UpdatedAt = DateTime.Now
            };
            fixtureDal.Add(fixture);
            return new SuccessDataResult<Guid>(fixture.Id, Messages.FixtureAdded);
        }

        [SecuredOperationAspect("Fixture.Delete")]
        [CacheRemoveAspect("IFixtureService.Get")]
        [CacheRemoveAspect("ICategoryService.GetFixtures")]
        [CacheRemoveAspect("ISupplierService.GetFixtures")]
        public IResult Delete(Guid fixtureId)
        {
            var fixture = GetById(fixtureId).Data;
            fixture.FixturePositionId = 0;
            fixture.UpdatedAt = DateTime.Now;

            fixtureDal.Update(fixture);
            return new SuccessResult(Messages.FixtureDeleted);
        }

        [SecuredOperationAspect("Fixture.Get")]
        [CacheAspect()]
        public IDataResult<Fixture> GetById(Guid fixtureId)
        {
            var fixture = fixtureDal.Get(f => f.Id == fixtureId);
            if (fixture != null)
                return new SuccessDataResult<Fixture>(fixture);
            throw new ObjectNotFoundException(Messages.FixtureNotFound);
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Fixture.GetDebits")]
        [CacheAspect(duration: 1)]
        public IDataResult<List<DebitForUserDetailDto>> GetDebits(Guid fixtureId)
        {
            var fixture = GetById(fixtureId).Data;
            return new SuccessDataResult<List<DebitForUserDetailDto>>(fixtureDal.GetDebits(fixture));
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Fixture.List")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Fixture>> GetList()
        {
            return new SuccessDataResult<List<Fixture>>(fixtureDal.GetList(f => f.FixturePositionId != (short)FixturePositions.Position.NotActive).ToList());
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Fixture.List")]
        [CacheAspect(duration: 2)]
        public List<Fixture> GetListByCategoryId(short categoryId)
        {
            return fixtureDal.GetList(f => f.CategoryId == categoryId).ToList();
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Fixture.List")]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Fixture>> GetListByPosition(FixturePositions.Position position)
        {
            return new SuccessDataResult<List<Fixture>>(fixtureDal.GetList(f => f.FixturePositionId == (short)position).ToList());
        }

        [PerformanceLogAspect(1, typeof(FileLogger))]
        [SecuredOperationAspect("Fixture.List")]
        [CacheAspect(duration: 2)]
        public List<Fixture> GetListBySupplierId(int supplierId)
        {
            return fixtureDal.GetList(f => f.SupplierId == supplierId).ToList();
        }

        [SecuredOperationAspect("Fixture.Update")]
        [ValidationAspect(typeof(FixtureForUpdateValidator))]
        [CacheRemoveAspect("IFixtureService.Get")]
        [CacheRemoveAspect("ICategoryService.GetFixtures")]
        [CacheRemoveAspect("ISupplierService.GetFixtures")]
        public IResult Update(FixtureForUpdateDto fixtureForUpdateDto)
        {
            var fixture = GetById(fixtureForUpdateDto.Id).Data;
            fixture.CategoryId = fixtureForUpdateDto.CategoryId;
            fixture.DatePurchase = fixtureForUpdateDto.DatePurchase;
            fixture.DateWarranty = fixtureForUpdateDto.DateWarranty;
            fixture.Description = fixtureForUpdateDto.Description;
            fixture.Name = fixtureForUpdateDto.Name;
            fixture.PictureUrl = fixtureForUpdateDto.PictureUrl;
            fixture.Price = fixtureForUpdateDto.Price;
            fixture.SupplierId = fixtureForUpdateDto.SupplierId;
            fixture.UpdatedAt = DateTime.Now;

            fixtureDal.Update(fixture);
            return new SuccessResult(Messages.FixtureUpdated);
        }
    }
}
