using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Entities.Dtos.Debit;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void GetById_WhenCalledNotExistUser_ShouldThrowObjectNotFoundExcepiton()
        {
            // Arrange
            var userId = Guid.Empty;
            var mockUserDal = new MockUserDal().MockGet(null);
            var sut = new UserManager(mockUserDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetById(userId));
        }

        [Fact]
        public void GetById_WhenCalledWithId_ShouldReturnUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mockUserDal = new MockUserDal().MockGet(new User());
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetById(userId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetByUsername_WhenCalledNotExistUser_ShouldThrowObjectNotFoundExcepiton()
        {
            // Arrange
            var username = "not.exists.user";
            var mockUserDal = new MockUserDal().MockGet(null);
            var sut = new UserManager(mockUserDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetByUsername(username));
        }

        [Fact]
        public void GetByUsername_WhenCalledWithUsername_ShouldReturnUser()
        {
            // Arrange
            var username = "user";
            var mockUserDal = new MockUserDal().MockGet(new User());
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetByUsername(username);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetByEmail_WhenCalledNotExistUser_ShouldThrowObjectNotFoundExcepiton()
        {
            // Arrange
            var email = "not-exists-user@mail.com";
            var mockUserDal = new MockUserDal().MockGet(null);
            var sut = new UserManager(mockUserDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetByEmail(email));
        }

        [Fact]
        public void GetByEmail_WhenCalledWithEmail_ShouldReturnUser()
        {
            // Arrange
            var email = "user@mail.com";
            var mockUserDal = new MockUserDal().MockGet(new User());
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetByEmail(email);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetList_WhenCalledAll_ShouldReturnUsers()
        {
            // Arrange
            var users = new List<User>()
            {
                new User(),
                new User(),
                new User()
            };
            var mockUserDal = new MockUserDal().MockGetList(users);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetDebits_WhenNotExistsUser_ShouldThrowObjectNotFoundExcepiton()
        {
            // Arrange
            var userId = Guid.Empty;
            var mockUserDal = new MockUserDal().MockGet(null);
            var sut = new UserManager(mockUserDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetDebits(userId));
        }

        [Fact]
        public void GetDebits_WhenCalledDebits_ShouldReturnDebitDtos()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mockUserDal = new MockUserDal().MockGetDebits(new List<DebitForFixtureDetailDto>()).MockGet(new User());
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetDebits(userId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetListByDepartmentId_WhenCalledWithDepartmentId_ShouldReturnUsers()
        {
            // Arrange
            int departmentId = 1;
            var users = new List<User>()
            {
                new User()
                {
                    DepartmentId = departmentId
                },
            };
            var mockUserDal = new MockUserDal().MockGetList(users);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetListByDepartmentId(departmentId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void IsAlreadyExistsEmail_WhenCalledEmailExists_ShouldReturnExistStatus()
        {
            // Arrange
            var existStatus = true;
            var email = "already-exists@mail.com";
            var mockUserDal = new MockUserDal().MockAny(existStatus);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.IsAlreadyExistsEmail(email);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAlreadyExistsUsername_WhenCalledUsernameExists_ShouldReturnExistStatus()
        {
            // Arrange
            var existStatus = false;
            var username = "not.exists";
            var mockUserDal = new MockUserDal().MockAny(existStatus);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.IsAlreadyExistsUsername(username);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Add_WhenAddedNewUser_ShouldAddAndReturnId()
        {
            // Arrange
            var user = new User();
            var mockUserDal = new MockUserDal().MockAdd(user);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.Add(user);

            // Assert
            Assert.Equal(new Guid(), result);
        }

        [Fact]
        public void Delete_WhenDeletedNotExistsUser_ShouldThrowObjectNotFoundExcepiton()
        {
            // Arrange
            var userId = Guid.Empty;
            var mockUserDal = new MockUserDal().MockUpdate().MockGet(null);
            var sut = new UserManager(mockUserDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Delete(userId));
        }

        [Fact]
        public void Delete_WhenDeletedUser_ShouldUpdateEnableStatus()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mockUserDal = new MockUserDal().MockUpdate().MockGet(new User());
            var sut = new UserManager(mockUserDal.Object);

            // Act
            sut.Delete(userId);

            // Assert
            mockUserDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void GetClaimsForLogin_WhenCalledClaimsForLogin_ShouldReturnClaimNameArray()
        {
            // Arrange
            string[] claimNames = { "Fixture.Get", "Fixture.List" };
            var user = new User();
            var mockUserDal = new MockUserDal().MockGetClaims(claimNames);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetClaimsForLogin(user);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetUserByEmailForLogin_WhenCalledUserForLogin_ShouldReturnUser()
        {
            // Arrange
            var email = "user@mail.com";
            var mockUserDal = new MockUserDal().MockGet(new User());
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetUserByEmailForLogin(email);

            // Assert
            Assert.NotNull(result);
        }

    }
}
