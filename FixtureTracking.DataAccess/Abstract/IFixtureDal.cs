using FixtureTracking.Core.DataAccess;
using FixtureTracking.Entities.Concrete;

namespace FixtureTracking.DataAccess.Abstract
{
    public interface IFixtureDal : IEntityRepository<Fixture>
    {
    }
}
