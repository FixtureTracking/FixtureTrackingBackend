using FixtureTracking.Core.DataAccess;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IDebitDal : IEntityRepository<Debit>
    {
        List<DebitForDetailDto> GetDetailList();
    }
}
