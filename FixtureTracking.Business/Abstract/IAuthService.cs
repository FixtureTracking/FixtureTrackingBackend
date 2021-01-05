using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Core.Utilities.Security.Tokens;
using FixtureTracking.Entities.Dtos.User;
using System;

namespace FixtureTracking.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto);
        IDataResult<Guid> Register(UserForRegisterDto userForRegisterDto);
    }
}
