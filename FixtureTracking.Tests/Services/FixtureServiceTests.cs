using FixtureTracking.Business.Concrete;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Fixture;
using FixtureTracking.Tests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Tests.Services
{
    public class FixtureServiceTests
    {
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
        public void GetById_WhenCalledWithNotExistsId_ShouldReturnNull()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            var mockFixtureDal = new MockFixtureDal().MockGet(null);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetById(fixtureId);

            // Assert
            Assert.Null(result.Data);
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
        public void GetListBySupplierId_WhenCalledWithNotExistsSupplierId_ShouldReturnEmptyList()
        {
            // Arrange
            int supplierId = 1;
            List<Fixture> fixtures = new List<Fixture>();
            var mockFixtureDal = new MockFixtureDal().MockGetList(fixtures);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListBySupplierId(supplierId);

            // Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public void GetListByCategoryId_WhenCalledWithCategoryId_ShouldReturnFixtures()
        {
            // Arrange
            short categoryId = 2;
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
        public void GetListByCompanyId_WhenCalledWithCompanyId_ShouldReturnFixtures()
        {
            // Arrange
            short companyId = 3;
            Fixture fixture = new Fixture()
            {
                Id = Guid.NewGuid(),
                CompanyId = companyId
            };
            List<Fixture> fixtures = new List<Fixture>
            {
                fixture
            };
            var mockFixtureDal = new MockFixtureDal().MockGetList(fixtures);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListByCompanyId(companyId);

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Add_WhenAddedNewFixture_ShouldAddedAndReturnId()
        {
            // Arrange
            FixtureForAddDto fixtureForAddDto = new FixtureForAddDto();
            Fixture fixture = new Fixture();
            var mockFixtureDal = new MockFixtureDal().MockAdd(fixture);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.Add(fixtureForAddDto);

            // Assert
            Assert.Equal(Guid.Empty, result.Data);
        }

        [Fact]
        public void Update_WhenUpdatedFixture_ShouldUpdate()
        {
            // Arrange
            FixtureForUpdateDto fixtureForUpdateDto = new FixtureForUpdateDto();
            var mockFixtureDal = new MockFixtureDal().MockUpdate();
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            sut.Update(fixtureForUpdateDto);

            // Assert
            mockFixtureDal.VerifyUpdate(Times.Once());
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

        [Fact]
        public void Delete_WhenDeletedNotExistsFixture_ShouldReturnErrorResult()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            var mockFixtureDal = new MockFixtureDal().MockDelete().MockGet(null);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.Delete(fixtureId);

            // Assert
            mockFixtureDal.VerifyDelete(Times.Never());
            Assert.False(result.Success);
        }
    }
}
