using FixtureTracking.Core.Entities.Concrete;

namespace FixtureTracking.Core.Utilities.Security.Tokens
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, string[] claimNames);
    }
}
