using FixtureTracking.Business.Abstract;
using FixtureTracking.Core.Entities.Concrete;
using Moq;

namespace FixtureTracking.Business.Tests.Mocks.Services
{
    public class MockUserService : Mock<IUserService>
    {
        public MockUserService MockIsAlreadyExistsEmail(bool result)
        {
            Setup(x => x.IsAlreadyExistsEmail(It.IsAny<string>()))
                .Returns(result);

            return this;
        }

        public MockUserService MockIsAlreadyExistsUsername(bool result)
        {
            Setup(x => x.IsAlreadyExistsUsername(It.IsAny<string>()))
                .Returns(result);

            return this;
        }

        public MockUserService MockGetClaimsForLogin(string[] result)
        {
            Setup(x => x.GetClaimsForLogin(It.IsAny<User>()))
                .Returns(result);

            return this;
        }

        public MockUserService MockGetUserByEmailForLogin(User result)
        {
            Setup(x => x.GetUserByEmailForLogin(It.IsAny<string>()))
                .Returns(result);

            return this;
        }
    }
}
