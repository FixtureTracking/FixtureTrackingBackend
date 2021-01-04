using FixtureTracking.Core.Entities.Concrete;
using Microsoft.Extensions.Configuration;
using System;

namespace FixtureTracking.Core.Utilities.Security.Tokens.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private readonly TokenOptions tokenOptions;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            tokenOptions = Configuration.GetSection("Tokenoptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, string[] claimNames)
        {
            throw new NotImplementedException();
        }
    }
}
