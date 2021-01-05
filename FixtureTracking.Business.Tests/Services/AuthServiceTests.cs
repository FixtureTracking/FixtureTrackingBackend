using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.Tests.Mocks.Helpers;
using FixtureTracking.Business.Tests.Mocks.Services;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Core.Utilities.Security.Hashing;
using FixtureTracking.Entities.Dtos.User;
using System;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public void Register_WhenEmailAlreadyExists_ShouldErrorResult()
        {
            // Arrange
            var userForRegisterDto = new UserForRegisterDto() { Email = "user@mail.com" };
            var dataResult = new SuccessDataResult<User>(new User());
            var mockUserService = new MockUserService().MockGetByEMail(dataResult);
            var sut = new AuthManager(mockUserService.Object, null);

            // Act
            var result = sut.Register(userForRegisterDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(Messages.AuthEmailExists, result.Message);
        }

        [Fact]
        public void Register_WhenUsernameAlreadyExists_ShouldErrorResult()
        {
            // Arrange
            var userForRegisterDto = new UserForRegisterDto() { Username = "user" };
            var emailDataResult = new SuccessDataResult<User>(data: null);
            var usernameDataResult = new SuccessDataResult<User>(new User());
            var mockUserService = new MockUserService().MockGetByEMail(emailDataResult).MockGetByUsername(usernameDataResult);
            var sut = new AuthManager(mockUserService.Object, null);

            // Act
            var result = sut.Register(userForRegisterDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(Messages.AuthUsernameExists, result.Message);
        }

        [Fact]
        public void Register_WhenRegisteredNewUser_ShouldRegisterAndReturnId()
        {
            // Arrange
            var userForRegisterDto = new UserForRegisterDto()
            {
                Email = "new-user@mail.com",
                Username = "new-user",
                Password = "pAS$w0rD"
            };
            var emailDataResult = new SuccessDataResult<User>(data: null);
            var usernameDataResult = new SuccessDataResult<User>(data: null);
            var mockUserService = new MockUserService().MockGetByEMail(emailDataResult).MockGetByUsername(usernameDataResult);
            var sut = new AuthManager(mockUserService.Object, null);

            // Act
            var result = sut.Register(userForRegisterDto);

            // Assert
            Assert.Equal(new Guid(), result.Data);
        }

        [Fact]
        public void Login_WhenNotExistsEmail_ShouldReturnErrorResult()
        {
            // Arrange
            var userForLoginDto = new UserForLoginDto() { Email = "not.exists@mail.com" };
            var dataResult = new SuccessDataResult<User>(data: null);
            var mockUserService = new MockUserService().MockGetByEMail(dataResult);
            var sut = new AuthManager(mockUserService.Object, null);

            // Act
            var result = sut.Login(userForLoginDto);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Login_WhenWrongPassword_ShouldReturnErrorResult()
        {
            // Arrange
            var userForLoginDto = new UserForLoginDto() { Email = "user@mail.com", Password = "password" };
            var user = new User() { PasswordHash = new byte[1], PasswordSalt = new byte[1] };
            var dataResult = new SuccessDataResult<User>(user);
            var mockUserService = new MockUserService().MockGetByEMail(dataResult);
            var sut = new AuthManager(mockUserService.Object, null);

            // Act
            var result = sut.Login(userForLoginDto);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void Login_WhenLogined_ShouldReturnAccessToken()
        {
            // Arrange
            var userForLoginDto = new UserForLoginDto() { Email = "user@mail.com", Password = "Pa$$w0RD" };
            HashingHelper.CreatePasswordHash(userForLoginDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User() { PasswordHash = passwordHash, PasswordSalt = passwordSalt };
            var emailDataResult = new SuccessDataResult<User>(user);
            var claimsDataResult = new SuccessDataResult<string[]>(data: Array.Empty<string>());
            var mockUserService = new MockUserService().MockGetByEMail(emailDataResult).MockGetClaims(claimsDataResult);
            var mockTokenHelper = new MockTokenHelper().MockAccessToken(new Core.Utilities.Security.Tokens.AccessToken());
            var sut = new AuthManager(mockUserService.Object, mockTokenHelper.Object);

            // Act
            var result = sut.Login(userForLoginDto);

            // Assert
            Assert.NotNull(result.Data);
        }
    }
}
