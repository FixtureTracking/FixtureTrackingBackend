using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using System;
using System.Collections.Generic;

namespace FixtureTracking.Business.Abstract
{
    public interface IDebitService
    {
        IDataResult<Debit> GetById(Guid debitId);
        IDataResult<List<DebitForDetailDto>> GetList();
        List<Debit> GetListByFixtureId(Guid fixtureId);
        List<Debit> GetListByUserId(Guid userId);
        IDataResult<Guid> Add(DebitForAddDto debitForAddDto);
        IResult Update(DebitForUpdateDto debitForUpdateDto);
        IResult Delete(Guid debitId);
    }
}
