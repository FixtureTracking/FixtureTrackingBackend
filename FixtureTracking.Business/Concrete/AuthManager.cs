using FixtureTracking.Business.Abstract;
using FixtureTracking.Business.BusinessAspects.Autofac;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.AuthValidations;
using FixtureTracking.Core.Aspects.Autofac.Validation;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.CustomExceptions;
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

        [ValidationAspect(typeof(UserForLoginValidator))]
        public IDataResult<AccessToken> Login(UserForLoginDto userForLoginDto)
        {
            var user = userService.GetUserByEmailForLogin(userForLoginDto.Email);
            if (user == null)
                throw new ObjectNotFoundException(Messages.AuthUserNotFound);
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                throw new ObjectNotFoundException(Messages.AuthUserNotFound);

            var claims = userService.GetClaimsForLogin(user);
            var accessToken = tokenHelper.CreateToken(user, claims);

            return new SuccessDataResult<AccessToken>(accessToken);
        }

        [SecuredOperationAspect("Auth.Register")]
        [ValidationAspect(typeof(UserForRegisterValidator))]
        public IDataResult<Guid> Register(UserForRegisterDto userForRegisterDto)
        {
            if (userService.IsAlreadyExistsEmail(userForRegisterDto.Email))
                throw new ObjectAlreadyExistsException(Messages.AuthEmailExists);

            if (userService.IsAlreadyExistsUsername(userForRegisterDto.Username))
                throw new ObjectAlreadyExistsException(Messages.AuthUsernameExists);

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
