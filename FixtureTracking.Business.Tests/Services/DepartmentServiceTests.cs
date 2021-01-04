﻿using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Department;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class DepartmentServiceTests
    {
        [Fact]
        public void GetById_WhenCalledWithNotExistsId_ShouldReturnNull()
        {
            // Arrange
            int departmentId = 111;
            var mockDepartmentDal = new MockDepartmentDal().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.GetById(departmentId);

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetById_WhenCalledWithId_ShouldReturnDepartment()
        {
            // Arrange
            int departmentId = 1;
            var mockDepartmentDal = new MockDepartmentDal().MockGet(new Department());
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.GetById(departmentId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetOperationClaimNames_WhenNotExistsDepartment_ShouldReturnErrorResult()
        {
            // Arrange
            int departmentId = 111;
            var mockDepartmentDal = new MockDepartmentDal().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.GetOperationClaimNames(departmentId);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void GetOperationClaimNames_WhenCalledClaimNames_ShouldReturnClaimNames()
        {
            // Arrange
            int departmentId = 1;
            string[] operationClaimNames = { "Department.GetById", "Department.GetList" };
            var department = new Department()
            {
                OperationClaimNames = operationClaimNames
            };
            var mockDepartmentDal = new MockDepartmentDal().MockGet(department);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.GetOperationClaimNames(departmentId);

            // Assert
            Assert.Equal(operationClaimNames, result.Data);
        }

        [Fact]
        public void GetList_WhenThereIsNoDepartment_ShouldReturnEmptyList()
        {
            // Arrange
            var mockDepartmentDal = new MockDepartmentDal().MockGetList(new List<Department>());
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public void GetList_WhenCalledAll_ShouldReturnDepartments()
        {
            // Arrange
            var departments = new List<Department>() {
                new Department(),
                new Department()
            };
            var mockDepartmentDal = new MockDepartmentDal().MockGetList(departments);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Add_WhenAddedNewDepartment_ShouldAddAndReturnId()
        {
            // Arrange
            var departmentForAddDto = new DepartmentForAddDto();
            var mockDepartmentDal = new MockDepartmentDal().MockAdd(new Department());
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.Add(departmentForAddDto);

            // Assert
            Assert.Equal(new int(), result.Data);
        }

        [Fact]
        public void Update_WhenUpdatedNotExistsDepartment_ShouldReturnErrorResult()
        {
            // Arrange
            var departmentForUpdateDto = new DepartmentForUpdateDto();
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.Update(departmentForUpdateDto);

            // Assert
            mockDepartmentDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Update_WhenUpdatedDepartment_ShouldUpdate()
        {
            // Arrange
            var departmentForUpdateDto = new DepartmentForUpdateDto();
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(new Department());
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            sut.Update(departmentForUpdateDto);

            // Assert
            mockDepartmentDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void UpdateOperationClaim_WhenUpdatedNotExistsDepartment_ShouldReturnErrorResult()
        {
            // Arrange
            var departmentForUpdateClaimDto = new DepartmentForUpdateClaimDto();
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.UpdateOperationClaim(departmentForUpdateClaimDto);

            // Assert
            mockDepartmentDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void UpdateOperationClaim_WhenUpdatedUpdateClaim_ShouldUpdate()
        {
            // Arrange
            var departmentForUpdateClaimDto = new DepartmentForUpdateClaimDto();
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(new Department());
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            sut.UpdateOperationClaim(departmentForUpdateClaimDto);

            // Assert
            mockDepartmentDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Delete_WhenDeletedNotExistsDepartment_ShouldReturnErrorResult()
        {
            // Arrange
            int departmentId = 111;
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.Delete(departmentId);

            // Assert
            mockDepartmentDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Delete_WhenDeletedDepartment_ShouldUpdateEnableStatus()
        {
            // Arrange
            var departmentId = 1;
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(new Department());
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            sut.Delete(departmentId);

            // Assert
            mockDepartmentDal.VerifyUpdate(Times.Once());
        }
    }
}