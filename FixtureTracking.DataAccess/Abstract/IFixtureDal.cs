using FixtureTracking.Core.DataAccess;
using FixtureTracking.Entities.Concrete;
using System.Collections.Generic;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IFixtureDal : IEntityRepository<Fixture>
    {
        List<Debit> GetDebits(Fixture fixture);
    }
}
