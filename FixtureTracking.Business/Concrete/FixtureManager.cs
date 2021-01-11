using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.FixtureValidations;
using FixtureTracking.Core.Aspects.Autofac.Caching;
using FixtureTracking.Core.Aspects.Autofac.Validation;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;
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

        [SecuredOperationAspect("Fixture.Add", Priority = 1)]
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

        [SecuredOperationAspect("Fixture.Delete", Priority = 1)]
        [CacheRemoveAspect("IFixtureService.Get")]
        [CacheRemoveAspect("ICategoryService.GetFixtures")]
        [CacheRemoveAspect("ISupplierService.GetFixtures")]
        public IResult Delete(Guid fixtureId)
        {
            var fixture = GetById(fixtureId).Data;
            if (fixture != null)
            {
                fixture.FixturePositionId = 0;
                fixture.UpdatedAt = DateTime.Now;

                fixtureDal.Update(fixture);
                return new SuccessResult(Messages.FixtureDeleted);
            }
            return new ErrorResult(Messages.FixtureNotFound);
        }

        [SecuredOperationAspect("Fixture.Get", Priority = 1)]
        [CacheAspect()]
        public IDataResult<Fixture> GetById(Guid fixtureId)
        {
            return new SuccessDataResult<Fixture>(fixtureDal.Get(f => f.Id == fixtureId));
        }

        [SecuredOperationAspect("Fixture.GetDebits", Priority = 1)]
        [CacheAspect(duration: 1)]
        public IDataResult<List<Debit>> GetDebits(Guid fixtureId)
        {
            var fixture = GetById(fixtureId).Data;
            if (fixture != null)
                return new SuccessDataResult<List<Debit>>(fixtureDal.GetDebits(fixture));
            return new ErrorDataResult<List<Debit>>(Messages.FixtureNotFound);
        }

        [SecuredOperationAspect("Fixture.List", Priority = 1)]
        [CacheAspect(duration: 2)]
        public IDataResult<List<Fixture>> GetList()
        {
            return new SuccessDataResult<List<Fixture>>(fixtureDal.GetList().ToList());
        }

        [SecuredOperationAspect("Fixture.List", Priority = 1)]
        [CacheAspect(duration: 2)]
        public List<Fixture> GetListByCategoryId(short categoryId)
        {
            return fixtureDal.GetList(f => f.CategoryId == categoryId).ToList();
        }

        [SecuredOperationAspect("Fixture.List", Priority = 1)]
        [CacheAspect(duration: 2)]
        public List<Fixture> GetListByPositionId(short positionId)
        {
            return fixtureDal.GetList(f => f.FixturePositionId == positionId).ToList();
        }

        [SecuredOperationAspect("Fixture.List", Priority = 1)]
        [CacheAspect(duration: 2)]
        public List<Fixture> GetListBySupplierId(int supplierId)
        {
            return fixtureDal.GetList(f => f.SupplierId == supplierId).ToList();
        }

        [SecuredOperationAspect("Fixture.Update", Priority = 1)]
        [ValidationAspect(typeof(FixtureForUpdateValidator))]
        [CacheRemoveAspect("IFixtureService.Get")]
        [CacheRemoveAspect("ICategoryService.GetFixtures")]
        [CacheRemoveAspect("ISupplierService.GetFixtures")]
        public IResult Update(FixtureForUpdateDto fixtureForUpdateDto)
        {
            var fixture = GetById(fixtureForUpdateDto.Id).Data;
            if (fixture != null)
            {
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
            return new ErrorResult(Messages.FixtureNotFound);
        }
    }
}
