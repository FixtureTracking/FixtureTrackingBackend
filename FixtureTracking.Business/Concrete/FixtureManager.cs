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
        private readonly IDebitService debitService;

        public FixtureManager(IFixtureDal fixtureDal, IDebitService debitService)
        {
            this.fixtureDal = fixtureDal;
            this.debitService = debitService;
        }

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

        public IDataResult<Fixture> GetById(Guid fixtureId)
        {
            return new SuccessDataResult<Fixture>(fixtureDal.Get(f => f.Id == fixtureId));
        }

        public IDataResult<List<Debit>> GetDebits(Guid fixtureId)
        {
            var fixture = GetById(fixtureId).Data;
            if (fixture != null)
                return new SuccessDataResult<List<Debit>>(debitService.GetListByFixtureId(fixtureId));
            return new ErrorDataResult<List<Debit>>(Messages.FixtureNotFound);
        }

        public IDataResult<List<Fixture>> GetList()
        {
            return new SuccessDataResult<List<Fixture>>(fixtureDal.GetList().ToList());
        }

        public List<Fixture> GetListByCategoryId(short categoryId)
        {
            return fixtureDal.GetList(f => f.CategoryId == categoryId).ToList();
        }

        public List<Fixture> GetListByPositionId(short positionId)
        {
            return fixtureDal.GetList(f => f.FixturePositionId == positionId).ToList();
        }

        public List<Fixture> GetListBySupplierId(int supplierId)
        {
            return fixtureDal.GetList(f => f.SupplierId == supplierId).ToList();
        }

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
