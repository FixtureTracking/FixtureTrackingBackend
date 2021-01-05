using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Security.Tokens;
using Moq;

namespace FixtureTracking.Business.Tests.Mocks.Helpers
{
    public class MockTokenHelper : Mock<ITokenHelper>
    {
        public MockTokenHelper MockAccessToken(AccessToken result)
        {
            Setup(x => x.CreateToken(It.IsAny<User>(), It.IsAny<string[]>()))
                .Returns(result);

            return this;
        }
    }
}
