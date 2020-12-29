using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
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

        public IDataResult<Guid> Add(FixtureForAddDto fixtureForAddDto)
        {
            var fixture = new Fixture()
            {
                // TODO : refactoring
                CategoryId = fixtureForAddDto.CategoryId,
                CompanyId = fixtureForAddDto.CompanyId,
                CreatedAt = DateTime.Now,
                DatePurchase = fixtureForAddDto.DatePurchase,
                DateWarranty = fixtureForAddDto.DateWarranty,
                Description = fixtureForAddDto.Description,
                IsActive = true,
                IsEnable = true,
                Name = fixtureForAddDto.Name,
                PictureUrl = fixtureForAddDto.PictureUrl,
                Price = fixtureForAddDto.Price,
                SupplierId = fixtureForAddDto.SupplierId,
                UpdatedAt = DateTime.Now
            };
            fixtureDal.Add(fixture);
            return new SuccessDataResult<Guid>(fixture.Id, Messages.FixtureAdded);
        }

        public IResult Delete(Guid fixtureId)
        {
            var fixture = GetById(fixtureId).Data;
            if (fixture != null)
            {
                fixtureDal.Delete(fixture);
                return new SuccessResult(Messages.FixtureDeleted);
            }
            return new ErrorResult(Messages.FixtureNotFound);
        }

        public IDataResult<Fixture> GetById(Guid fixtureId)
        {
            return new SuccessDataResult<Fixture>(fixtureDal.Get(f => f.Id == fixtureId));
        }

        public IDataResult<List<Fixture>> GetList()
        {
            return new SuccessDataResult<List<Fixture>>(fixtureDal.GetList().ToList());
        }

        public IDataResult<List<Fixture>> GetListByCategoryId(short categoryId)
        {
            return new SuccessDataResult<List<Fixture>>(fixtureDal.GetList(f => f.CategoryId == categoryId).ToList());
        }

        public IDataResult<List<Fixture>> GetListByCompanyId(short companyId)
        {
            return new SuccessDataResult<List<Fixture>>(fixtureDal.GetList(f => f.CompanyId == companyId).ToList());
        }

        public IDataResult<List<Fixture>> GetListBySupplierId(int supplierId)
        {
            return new SuccessDataResult<List<Fixture>>(fixtureDal.GetList(f => f.SupplierId == supplierId).ToList());
        }

        public IResult Update(Fixture fixture)
        {
            if (GetById(fixture.Id).Data != null)
            {
                fixture.UpdatedAt = DateTime.Now;
                fixtureDal.Update(fixture);
                return new SuccessResult(Messages.FixtureUpdated);
            }
            return new ErrorResult(Messages.FixtureNotFound);
        }
    }
}
