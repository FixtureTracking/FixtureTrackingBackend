using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;

namespace FixtureTracking.Business.Tests.Mocks
{
    public class MockUserDal : MockEntityRepository<User, IUserDal>
    {
    }
}
