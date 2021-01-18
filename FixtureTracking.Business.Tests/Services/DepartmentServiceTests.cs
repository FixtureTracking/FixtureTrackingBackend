using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Core.Utilities.CustomExceptions;
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
        public void GetById_WhenCalledNotExistsDepartment_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            int departmentId = 111;
            var mockDepartmentDal = new MockDepartmentDal().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetById(departmentId));
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
        public void GetOperationClaimNames_WhenNotExistsDepartment_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            int departmentId = 111;
            var mockDepartmentDal = new MockDepartmentDal().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetOperationClaimNames(departmentId));
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
        public void GetUsers_WhenNotExistsDepartment_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            int departmentId = 111;
            var mockDepartmentDal = new MockDepartmentDal().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetUsers(departmentId));
        }

        [Fact]
        public void GetUsers_WhenCalledUsers_ShouldReturnUsers()
        {
            // Arrange
            int departmentId = 1;
            var mockDepartmentDal = new MockDepartmentDal().MockGetUsers(new List<Core.Entities.Concrete.User>()).MockGet(new Department());
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act
            var result = sut.GetUsers(departmentId);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
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
        public void Update_WhenUpdatedNotExistsDepartment_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            var departmentForUpdateDto = new DepartmentForUpdateDto();
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Update(departmentForUpdateDto));
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
        public void UpdateOperationClaim_WhenUpdatedNotExistsDepartment_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            var departmentForUpdateClaimDto = new DepartmentForUpdateClaimDto();
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.UpdateOperationClaim(departmentForUpdateClaimDto));
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
        public void Delete_WhenDeletedNotExistsDepartment_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            int departmentId = 111;
            var mockDepartmentDal = new MockDepartmentDal().MockUpdate().MockGet(null);
            var sut = new DepartmentManager(mockDepartmentDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Delete(departmentId));
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
