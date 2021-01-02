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
        IDataResult<List<Fixture>> GetListBySupplierId(int supplierId);
        IDataResult<List<Fixture>> GetListByCategoryId(short categoryId);
        IDataResult<List<Fixture>> GetListByPositionId(short positionId);
        IDataResult<Guid> Add(FixtureForAddDto fixtureForAddDto);
        IResult Update(FixtureForUpdateDto fixtureForUpdateDto);
        IResult Delete(Guid fixtureId);
    }
}
