using FixtureTracking.DataAccess.Abstract;
using FixtureTracking.Entities.Concrete;

namespace FixtureTracking.Tests.Mocks
{
    public class MockFixtureDal : MockEntityRepository<Fixture, IFixtureDal>
    {
    }
}
