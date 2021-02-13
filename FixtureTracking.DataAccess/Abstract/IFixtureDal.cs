using FixtureTracking.Core.DataAccess;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.Fixture;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IFixtureDal : IEntityRepository<Fixture>
    {
        List<FixtureForDetailDto> GetDetailList();
        List<DebitForUserDetailDto> GetDebits(Fixture fixture);
    }
}
