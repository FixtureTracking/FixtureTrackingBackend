using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Core.Entities.Concrete;
using FixtureTracking.Entities.Concrete;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public void GetById_WhenCalledWithNotExistId_ShouldReturnNull()
        {
            // Arrange
            var userId = Guid.Empty;
            var mockUserDal = new MockUserDal().MockGet(null);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetById(userId);

            // Assert
            Assert.Null(result.Data);
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
        public void GetList_WhenThereIsNoUser_ShouldReturnEmptyList()
        {
            // Arrange
            var mockUserDal = new MockUserDal().MockGetList(new List<User>());
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.Empty(result.Data);
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
        public void GetClaims_WhenCalledClaims_ShouldReturnClaimNameArray()
        {
            // Arrange
            string[] claimNames = { "Fixture.GetById", "Fixture.GetList" };
            var user = new User();
            var mockUserDal = new MockUserDal().MockGetClaims(claimNames);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetClaims(user);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetDebits_WhenNotExistsUser_ShouldReturnErrorResult()
        {
            // Arrange
            var userId = Guid.Empty;
            var mockUserDal = new MockUserDal().MockGet(null);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetDebits(userId);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void GetDebits_WhenCalledDebits_ShouldReturnDebits()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var mockUserDal = new MockUserDal().MockGetDebits(new List<Debit>()).MockGet(new User());
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.GetDebits(userId);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
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
        public void Delete_WhenDeletedNotExistsUser_ShouldReturnErrorResult()
        {
            // Arrange
            var userId = Guid.Empty;
            var mockUserDal = new MockUserDal().MockUpdate().MockGet(null);
            var sut = new UserManager(mockUserDal.Object);

            // Act
            var result = sut.Delete(userId);

            // Assert
            mockUserDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
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

    }
}
