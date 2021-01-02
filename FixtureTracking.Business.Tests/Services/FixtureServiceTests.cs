using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class FixtureServiceTests
    {
        [Fact]
        public void GetById_WhenCalledWithNotExistsId_ShouldReturnNull()
        {
            // Arrange
            Guid fixtureId = Guid.Empty;
            var mockFixtureDal = new MockFixtureDal().MockGet(null);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetById(fixtureId);

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetById_WhenCalledWithId_ShouldReturnFixture()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            Fixture fixture = new Fixture
            {
                Id = fixtureId
            };
            var mockFixtureDal = new MockFixtureDal().MockGet(fixture);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetById(fixtureId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetList_WhenCalledAll_ShouldReturnFixtures()
        {
            // Arrange
            List<Fixture> fixtures = new List<Fixture>
            {
                new Fixture(),
                new Fixture()
            };
            var mockFixtureDal = new MockFixtureDal().MockGetList(fixtures);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetListBySupplierId_WhenCalledWithNotExistsSupplierId_ShouldReturnEmptyList()
        {
            // Arrange
            int supplierId = 111;
            List<Fixture> fixtures = new List<Fixture>();
            var mockFixtureDal = new MockFixtureDal().MockGetList(fixtures);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListBySupplierId(supplierId);

            // Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public void GetListBySupplierId_WhenCalledWithSupplierId_ShouldReturnFixtures()
        {
            // Arrange
            int supplierId = 1;
            Fixture fixture = new Fixture()
            {
                Id = Guid.NewGuid(),
                SupplierId = supplierId
            };
            List<Fixture> fixtures = new List<Fixture>
            {
                fixture
            };
            var mockFixtureDal = new MockFixtureDal().MockGetList(fixtures);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListBySupplierId(supplierId);

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetListByCategoryId_WhenCalledWithCategoryId_ShouldReturnFixtures()
        {
            // Arrange
            short categoryId = 1;
            Fixture fixture = new Fixture()
            {
                Id = Guid.NewGuid(),
                CategoryId = categoryId
            };
            List<Fixture> fixtures = new List<Fixture>
            {
                fixture
            };
            var mockFixtureDal = new MockFixtureDal().MockGetList(fixtures);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListByCategoryId(categoryId);

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetListByPositionId_WhenCalledWithPositionId_ShouldReturnFixtures()
        {
            // Arrange
            short positionId = 1;
            Fixture fixture = new Fixture()
            {
                Id = Guid.NewGuid(),
                FixturePositionId = positionId
            };
            List<Fixture> fixtures = new List<Fixture>
            {
                fixture
            };
            var mockFixtureDal = new MockFixtureDal().MockGetList(fixtures);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListByPositionId(positionId);

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Add_WhenAddedNewFixture_ShouldAddAndReturnId()
        {
            // Arrange
            FixtureForAddDto fixtureForAddDto = new FixtureForAddDto();
            var mockFixtureDal = new MockFixtureDal().MockAdd(new Fixture());
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.Add(fixtureForAddDto);

            // Assert
            Assert.Equal(new Guid(), result.Data);
        }

        [Fact]
        public void Update_WhenUpdatedNotExistsFixture_ShouldReturnErrorResult()
        {
            // Arrange
            var fixtureForUpdateDto = new FixtureForUpdateDto();
            var mockFixtureDal = new MockFixtureDal().MockUpdate().MockGet(null);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.Update(fixtureForUpdateDto);

            // Assert
            mockFixtureDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Update_WhenUpdatedFixture_ShouldUpdate()
        {
            // Arrange
            var fixtureForUpdateDto = new FixtureForUpdateDto();
            var mockFixtureDal = new MockFixtureDal().MockUpdate().MockGet(new Fixture());
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            sut.Update(fixtureForUpdateDto);

            // Assert
            mockFixtureDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Delete_WhenDeletedNotExistsFixture_ShouldReturnErrorResult()
        {
            // Arrange
            Guid fixtureId = Guid.Empty;
            var mockFixtureDal = new MockFixtureDal().MockDelete().MockGet(null);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.Delete(fixtureId);

            // Assert
            mockFixtureDal.VerifyDelete(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Delete_WhenDeletedFixture_ShouldDelete()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            Fixture fixture = new Fixture()
            {
                Id = fixtureId
            };
            var mockFixtureDal = new MockFixtureDal().MockDelete().MockGet(fixture);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            sut.Delete(fixtureId);

            // Assert
            mockFixtureDal.VerifyDelete(Times.Once());
        }
    }
}
