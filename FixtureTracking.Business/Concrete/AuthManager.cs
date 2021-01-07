using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.Constants;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Core.Utilities.Security.Hashing;
using FixtureTracking.Core.Utilities.Security.Tokens;
using FixtureTracking.Entities.Dtos.User;
using System;

namespace FixtureTracking.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService userService;
        private readonly ITokenHelper tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            this.userService = userService;
            this.tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto)
        {
            var user = userService.GetByEmail(userForLoginDto.Email).Data;
            if (user == null)
                return new ErrorDataResult<AccessToken>(Messages.AuthUserNotFound);
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                return new ErrorDataResult<AccessToken>(Messages.AuthUserNotFound);

            var claims = userService.GetClaims(user);
            var accessToken = tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<Guid> Register(UserForRegisterDto userForRegisterDto)
        {
            var isEmailExists = userService.GetByEmail(userForRegisterDto.Email).Data != null;
            if (isEmailExists)
                return new ErrorDataResult<Guid>(Messages.AuthEmailExists);

            var isUsernameExists = userService.GetByUsername(userForRegisterDto.Username).Data != null;
            if (isUsernameExists)
                return new ErrorDataResult<Guid>(Messages.AuthUsernameExists);

            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User()
            {
                BirthDate = userForRegisterDto.BirthDate,
                CreatedAt = DateTime.Now,
                DepartmentId = userForRegisterDto.DepartmentId,
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                IsEnable = true,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                UpdatedAt = DateTime.Now,
                Username = userForRegisterDto.Username
            };

            var userId = userService.Add(user);
            return new SuccessDataResult<Guid>(userId, Messages.AuthUserRegistered);
        }
    }
}
