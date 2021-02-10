using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Business.Tests.Mocks.Services;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Core.Utilities.Results;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Debit;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class DebitServiceTests
    {
        [Fact]
        public void GetById_WhenCalledNotExistsDebit_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            Guid debitId = Guid.Empty;
            var mockDebitDal = new MockDebitDal().MockGet(null);
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetById(debitId));
        }

        [Fact]
        public void GetById_WhenCalledWithId_ShouldReturnDebit()
        {
            // Arrange
            Guid debitId = Guid.NewGuid();
            Debit debit = new Debit()
            {
                Id = debitId
            };
            var mockDebitDal = new MockDebitDal().MockGet(debit);
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act
            var result = sut.GetById(debitId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetList_WhenCalledAll_ShouldReturnDebits()
        {
            // Arrange
            List<Debit> debits = new List<Debit>
            {
                new Debit(),
                new Debit(),
                new Debit()
            };
            var mockDebitDal = new MockDebitDal().MockGetList(debits);
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetListByFixtureId_WhenCalledWithFixtureId_ShouldReturnDebits()
        {
            // Arrange
            Guid fixtureId = Guid.NewGuid();
            Debit debit = new Debit()
            {
                Id = Guid.NewGuid(),
                FixtureId = fixtureId
            };
            List<Debit> debits = new List<Debit>
            {
                debit
            };

            var mockDebitDal = new MockDebitDal().MockGetList(debits);
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act
            var result = sut.GetListByFixtureId(fixtureId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetListByUserId_WhenCalledWithUserId_ShouldReturnDebits()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            Debit debit = new Debit()
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };
            List<Debit> debits = new List<Debit>
            {
                debit
            };

            var mockDebitDal = new MockDebitDal().MockGetList(debits);
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act
            var result = sut.GetListByUserId(userId);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Add_WhenFixturePositionIsNotAvailable_ShouldThrowLogicException()
        {
            // Arrange
            DebitForAddDto debitForAddDto = new DebitForAddDto();
            IDataResult<Fixture> fixtureDataResult = new SuccessDataResult<Fixture>(new Fixture() { FixturePositionId = 2 });
            var mockDebitDal = new MockDebitDal().MockAdd(new Debit());
            var mockFixtureService = new MockFixtureService().MockGetById(fixtureDataResult).MockUpdatePostiton(new SuccessResult());
            var sut = new DebitManager(mockDebitDal.Object, mockFixtureService.Object);

            // Act & Assert
            Assert.Throws<LogicException>(() => sut.Add(debitForAddDto));
        }

        [Fact]
        public void Add_WhenAddedNewDebit_ShouldAddAndReturnId()
        {
            // Arrange
            DebitForAddDto debitForAddDto = new DebitForAddDto();
            IDataResult<Fixture> fixtureDataResult = new SuccessDataResult<Fixture>(new Fixture() { FixturePositionId = 1 });
            var mockDebitDal = new MockDebitDal().MockAdd(new Debit());
            var mockFixtureService = new MockFixtureService().MockGetById(fixtureDataResult).MockUpdatePostiton(new SuccessResult());
            var sut = new DebitManager(mockDebitDal.Object, mockFixtureService.Object);

            // Act
            var result = sut.Add(debitForAddDto);

            // Assert
            Assert.Equal(new Guid(), result.Data);
        }

        [Fact]
        public void Update_WhenUpdatedNotExistsDebit_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            var debitForUpdateDto = new DebitForUpdateDto();
            var mockDebitDal = new MockDebitDal().MockUpdate().MockGet(null);
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Update(debitForUpdateDto));
        }

        [Fact]
        public void Update_WhenUpdatedDebit_ShouldUpdate()
        {
            // Arrange
            var debitForUpdateDto = new DebitForUpdateDto();
            var mockDebitDal = new MockDebitDal().MockUpdate().MockGet(new Debit());
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act
            sut.Update(debitForUpdateDto);

            // Assert
            mockDebitDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Delete_WhenDeletedNotExistsDebit_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            Guid debitId = Guid.Empty;
            var mockDebitDal = new MockDebitDal().MockUpdate().MockGet(null);
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Delete(debitId));
        }

        [Fact]
        public void Delete_WhenDeletedDebit_ShouldUpdateReturnStatus()
        {
            // Arrange
            Guid debitId = Guid.NewGuid();
            Debit debit = new Debit()
            {
                Id = debitId
            };
            var mockDebitDal = new MockDebitDal().MockUpdate().MockGet(debit);
            var sut = new DebitManager(mockDebitDal.Object, null);

            // Act
            sut.Delete(debitId);

            // Assert
            mockDebitDal.VerifyUpdate(Times.Once());
        }
    }
}
