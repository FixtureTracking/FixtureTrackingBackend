﻿using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks;
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
        public void GetById_WhenCalledWithId_ShouldReturnDebit()
        {
            // Arrange
            Guid debitId = Guid.NewGuid();
            Debit debit = new Debit()
            {
                Id = debitId
            };
            var mockDebitDal = new MockDebitDal().MockGet(debit);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.GetById(debitId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetById_WhenCalledWithNotExistsId_ShouldReturnNull()
        {
            // Arrange
            Guid debitId = Guid.NewGuid();
            var mockDebitDal = new MockDebitDal().MockGet(null);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.GetById(debitId);

            // Assert
            Assert.Null(result.Data);
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
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetList_WhenThereIsNoDebit_ShouldReturnEmptyList()
        {
            // Arrange
            List<Debit> debits = new List<Debit>();
            var mockDebitDal = new MockDebitDal().MockGetList(debits);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.Empty(result.Data);
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
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.GetListByFixtureId(fixtureId);

            // Assert
            Assert.NotEmpty(result.Data);
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
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.GetListByUserId(userId);

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetListByDepartmentId_WhenCalledWithDepartmentId_ShouldReturnDebits()
        {
            // Arrange
            short departmentId = 1;
            Debit debit = new Debit()
            {
                Id = Guid.NewGuid(),
                DepartmentId = departmentId
            };
            List<Debit> debits = new List<Debit>
            {
                debit
            };

            var mockDebitDal = new MockDebitDal().MockGetList(debits);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.GetListByDepartmentId(departmentId);

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetListByDepartmentId_WhenCalledWithNotExistsDepartmentId_ShouldReturnEmptyList()
        {
            // Arrange
            short departmentId = 1;
            List<Debit> debits = new List<Debit>();
            var mockDebitDal = new MockDebitDal().MockGetList(debits);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.GetListByDepartmentId(departmentId);

            // Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public void Add_WhenAddedNewDebit_ShouldAddedAndReturnId()
        {
            // Arrange
            DebitForAddDto debitForAddDto = new DebitForAddDto();
            Debit debit = new Debit();
            var mockDebitDal = new MockDebitDal().MockAdd(debit);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.Add(debitForAddDto);

            // Assert
            Assert.Equal(Guid.Empty, result.Data);
        }

        [Fact]
        public void Update_WhenUpdatedDebit_ShouldUpdate()
        {
            // Arrange
            Debit debit = new Debit();
            var mockDebitDal = new MockDebitDal().MockUpdate().MockGet(debit);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            sut.Update(debit);

            // Assert
            mockDebitDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Update_WhenUpdatedNotExistsDebit_ShouldReturnErrorResult()
        {
            // Arrange
            Debit debit = new Debit();
            var mockDebitDal = new MockDebitDal().MockUpdate().MockGet(null);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.Update(debit);

            // Assert
            mockDebitDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Delete_WhenDeletedDebit_ShouldDelete()
        {
            // Arrange
            Guid debitId = Guid.NewGuid();
            Debit debit = new Debit()
            {
                Id = debitId
            };
            var mockDebitDal = new MockDebitDal().MockDelete().MockGet(debit);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            sut.Delete(debitId);

            // Assert
            mockDebitDal.VerifyDelete(Times.Once());
        }

        [Fact]
        public void Delete_WhenDeletedNotExistsDebit_ShouldReturnErrorResult()
        {
            // Arrange
            Guid debitId = Guid.NewGuid();
            var mockDebitDal = new MockDebitDal().MockDelete().MockGet(null);
            var sut = new DebitManager(mockDebitDal.Object);

            // Act
            var result = sut.Delete(debitId);

            // Assert
            mockDebitDal.VerifyDelete(Times.Never());
            Assert.False(result.Success);
        }

    }
}
