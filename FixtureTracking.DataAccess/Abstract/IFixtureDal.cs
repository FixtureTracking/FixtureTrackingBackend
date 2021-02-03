using FixtureTracking.Core.DataAccess;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IFixtureDal : IEntityRepository<Fixture>
    {
        List<DebitForUserDetailDto> GetDebits(Fixture fixture);
    }
}
