using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.Fixture;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface IFixtureService
    {
        IDataResult<Fixture> GetById(Guid fixtureId);
        IDataResult<List<FixtureForDetailDto>> GetList();
        IDataResult<List<Fixture>> GetListByPosition(FixturePositions.Position position);
        IDataResult<List<DebitForUserDetailDto>> GetDebits(Guid fixtureId);
        List<Fixture> GetListBySupplierId(int supplierId);
        List<Fixture> GetListByCategoryId(short categoryId);
        IDataResult<Guid> Add(FixtureForAddDto fixtureForAddDto);
        IResult Update(FixtureForUpdateDto fixtureForUpdateDto);
        IResult UpdatePosition(Guid fixtureId, FixturePositions.Position position);
        IResult Delete(Guid fixtureId);
    }
}
