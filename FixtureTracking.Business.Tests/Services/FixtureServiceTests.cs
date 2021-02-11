using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Constants;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
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
        public void GetById_WhenCalledNotExistsFixture_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            Guid fixtureId = Guid.Empty;
            var mockFixtureDal = new MockFixtureDal().MockGet(null);
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetById(fixtureId));
        }

        [Fact]
        public void GetById_WhenCalledWithId_ShouldReturnFixture()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            var mockFixtureDal = new MockFixtureDal().MockGet(new Fixture());
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
            var mockFixtureDal = new MockFixtureDal().MockGetList(new List<Fixture>());
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetListByPosition_WhenCalledWithPosition_ShouldReturnFixtures()
        {
            // Arrange
            var position = FixturePositions.Position.Available;
            var mockFixtureDal = new MockFixtureDal().MockGetList(new List<Fixture>());
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListByPosition(position);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetDebits_WhenCalledDebits_ReturnDebitDtos()
        {
            // Arrange
            var fixtureId = Guid.NewGuid();
            var mockFixtureDal = new MockFixtureDal().MockGetDebits(new List<DebitForUserDetailDto>()).MockGet(new Fixture());
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetDebits(fixtureId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetListBySupplierId_WhenCalledWithSupplierId_ShouldReturnFixtures()
        {
            // Arrange
            int supplierId = 1;
            var mockFixtureDal = new MockFixtureDal().MockGetList(new List<Fixture>());
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListBySupplierId(supplierId);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetListByCategoryId_WhenCalledWithCategoryId_ShouldReturnFixtures()
        {
            // Arrange
            short categoryId = 1;
            var mockFixtureDal = new MockFixtureDal().MockGetList(new List<Fixture>());
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            var result = sut.GetListByCategoryId(categoryId);

            // Assert
            Assert.NotNull(result);
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
        public void UpdatePosition_WhenUpdatedFixturePosition_ShouldUpdatePosition()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            var position = FixturePositions.Position.Debit;
            var mockFixtureDal = new MockFixtureDal().MockUpdate().MockGet(new Fixture());
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            sut.UpdatePosition(fixtureId, position);

            // Assert
            mockFixtureDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Delete_WhenFixturePositionIsNotAvailable_ShouldThrowLogicException()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            var mockFixtureDal = new MockFixtureDal().MockUpdate().MockGet(new Fixture() { FixturePositionId = 2 });
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act & Assert
            Assert.Throws<LogicException>(() => sut.Delete(fixtureId));
        }

        [Fact]
        public void Delete_WhenDeletedFixture_ShouldUpdateFixturePosition()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            var mockFixtureDal = new MockFixtureDal().MockUpdate().MockGet(new Fixture() { FixturePositionId = 1 });
            var sut = new FixtureManager(mockFixtureDal.Object);

            // Act
            sut.Delete(fixtureId);

            // Assert
            mockFixtureDal.VerifyUpdate(Times.Once());
        }
    }
}
