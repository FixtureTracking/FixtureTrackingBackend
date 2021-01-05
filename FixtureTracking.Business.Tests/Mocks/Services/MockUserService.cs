using FixtureTracking.Business.Abstract;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Results;
using Moq;

namespace FixtureTracking.Business.Tests.Mocks.Services
{
    public class MockUserService : Mock<IUserService>
    {
        public MockUserService MockGetByEMail(IDataResult<User> result)
        {
            Setup(x => x.GetByEmail(It.IsAny<string>()))
                .Returns(result);

            return this;
        }

        public MockUserService MockGetByUsername(IDataResult<User> result)
        {
            Setup(x => x.GetByUsername(It.IsAny<string>()))
                .Returns(result);

            return this;
        }

        public MockUserService MockGetClaims(IDataResult<string[]> result)
        {
            Setup(x => x.GetClaims(It.IsAny<User>()))
                .Returns(result);

            return this;
        }
    }
}
