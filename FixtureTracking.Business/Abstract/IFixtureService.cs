using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Fixture;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface IFixtureService
    {
        IDataResult<Fixture> GetById(Guid fixtureId);
        IDataResult<List<Fixture>> GetList();
        IDataResult<List<Debit>> GetDebits(Guid fixtureId);
        List<Fixture> GetListBySupplierId(int supplierId);
        List<Fixture> GetListByCategoryId(short categoryId);
        List<Fixture> GetListByPositionId(short positionId);
        IDataResult<Guid> Add(FixtureForAddDto fixtureForAddDto);
        IResult Update(FixtureForUpdateDto fixtureForUpdateDto);
        IResult Delete(Guid fixtureId);
    }
}
