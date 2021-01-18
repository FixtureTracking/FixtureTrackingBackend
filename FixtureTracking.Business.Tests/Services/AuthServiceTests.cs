using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.Tests.Mocks.Helpers;
using FixtureTracking.Business.Tests.Mocks.Services;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.CustomExceptions;
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
            var mockUserService = new MockUserService().MockIsAlreadyExistsEmail(true);
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
            var mockUserService = new MockUserService().MockIsAlreadyExistsEmail(false).MockIsAlreadyExistsUsername(true);
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
                Username = "new.user",
                Password = "pAS$w0rD"
            };
            var mockUserService = new MockUserService().MockIsAlreadyExistsEmail(false).MockIsAlreadyExistsUsername(false);
            var sut = new AuthManager(mockUserService.Object, null);

            // Act
            var result = sut.Register(userForRegisterDto);

            // Assert
            Assert.Equal(new Guid(), result.Data);
        }

        [Fact]
        public void Login_WhenNotExistsEmail_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            var userForLoginDto = new UserForLoginDto() { Email = "not.exists@mail.com" };
            var mockUserService = new MockUserService().MockGetUserByEmailForLogin(null);
            var sut = new AuthManager(mockUserService.Object, null);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Login(userForLoginDto));
        }

        [Fact]
        public void Login_WhenWrongPassword_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            var userForLoginDto = new UserForLoginDto() { Email = "user@mail.com", Password = "password" };
            var user = new User() { PasswordHash = new byte[1], PasswordSalt = new byte[1] };
            var mockUserService = new MockUserService().MockGetUserByEmailForLogin(user);
            var sut = new AuthManager(mockUserService.Object, null);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Login(userForLoginDto));
        }

        [Fact]
        public void Login_WhenLogined_ShouldReturnAccessToken()
        {
            // Arrange
            var userForLoginDto = new UserForLoginDto() { Email = "user@mail.com", Password = "Pa$$w0RD" };
            HashingHelper.CreatePasswordHash(userForLoginDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User() { PasswordHash = passwordHash, PasswordSalt = passwordSalt };
            var mockUserService = new MockUserService().MockGetUserByEmailForLogin(user).MockGetClaimsForLogin(Array.Empty<string>());
            var mockTokenHelper = new MockTokenHelper().MockAccessToken(new Core.Utilities.Security.Tokens.AccessToken());
            var sut = new AuthManager(mockUserService.Object, mockTokenHelper.Object);

            // Act
            var result = sut.Login(userForLoginDto);

            // Assert
            Assert.NotNull(result.Data);
        }
    }
}
