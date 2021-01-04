using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockUserDal : MockEntityRepository<User, IUserDal>
    {
    }
}
