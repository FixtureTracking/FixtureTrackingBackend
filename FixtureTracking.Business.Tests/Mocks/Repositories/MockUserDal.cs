using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.DataAccess.Abstract;
using Moq;

namespace FixtureTracking.Business.Tests.Mocks.Repositories
{
    public class MockUserDal : MockEntityRepository<User, IUserDal>
    {
        public MockUserDal MockGetClaims(string[] result)
        {
            Setup(x => x.GetClaims(It.IsAny<User>()))
                .Returns(result);

            return this;
        }
    }
}
