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
        private readonly IFixtureDal _fixtureDal;

        public FixtureManager(IFixtureDal fixtureDal)
        {
            _fixtureDal = fixtureDal;
        }

        public IDataResult<Guid> Add(FixtureForAddDto fixtureForAddDto)
        {
            var fixture = new Fixture()
            {
                // TODO : refactoring
                CategoryId = fixtureForAddDto.CategoryId,
                CompanyId = fixtureForAddDto.CompanyId,
                CreatedAt = fixtureForAddDto.CreatedAt,
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
            _fixtureDal.Add(fixture);
            return new SuccessDataResult<Guid>(fixture.Id, Messages.FixtureAdded);
        }

        public IResult Delete(Guid fixtureId)
        {
            var fixture = GetById(fixtureId).Data;
            if (fixture != null)
            {
                _fixtureDal.Delete(fixture);
                return new SuccessResult(Messages.FixtureDeleted);
            }
            return new ErrorResult(Messages.FixtureNotFound);
        }

        public IDataResult<Fixture> GetById(Guid fixtureId)
        {
            return new SuccessDataResult<Fixture>(_fixtureDal.Get(f => f.Id == fixtureId));
        }

        public IDataResult<List<Fixture>> GetList()
        {
            return new SuccessDataResult<List<Fixture>>(_fixtureDal.GetList().ToList());
        }

        public IDataResult<List<Fixture>> GetListByCategoryId(short categoryId)
        {
            return new SuccessDataResult<List<Fixture>>(_fixtureDal.GetList(f => f.CategoryId == categoryId).ToList());
        }

        public IDataResult<List<Fixture>> GetListByCompanyId(short companyId)
        {
            return new SuccessDataResult<List<Fixture>>(_fixtureDal.GetList(f => f.CompanyId == companyId).ToList());
        }

        public IDataResult<List<Fixture>> GetListBySupplierId(int supplierId)
        {
            return new SuccessDataResult<List<Fixture>>(_fixtureDal.GetList(f => f.SupplierId == supplierId).ToList());
        }

        public IResult Update(FixtureForUpdateDto fixtureForUpdateDto)
        {
            var fixture = new Fixture()
            {
                Id = fixtureForUpdateDto.Id,
                CategoryId = fixtureForUpdateDto.CategoryId,
                CompanyId = fixtureForUpdateDto.CompanyId,
                CreatedAt = fixtureForUpdateDto.CreatedAt,
                DatePurchase = fixtureForUpdateDto.DatePurchase,
                DateWarranty = fixtureForUpdateDto.DateWarranty,
                Description = fixtureForUpdateDto.Description,
                IsActive = fixtureForUpdateDto.IsActive,
                IsEnable = true,
                Name = fixtureForUpdateDto.Name,
                PictureUrl = fixtureForUpdateDto.PictureUrl,
                Price = fixtureForUpdateDto.Price,
                SupplierId = fixtureForUpdateDto.SupplierId,
                UpdatedAt = DateTime.Now
            };
            _fixtureDal.Update(fixture);
            return new SuccessResult(Messages.FixtureUpdated);
        }
    }
}
