using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Supplier;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class SupplierServiceTests
    {
        [Fact]
        public void GetById_WhenCalledNotExistsSupplier_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            int supplierId = 111;
            var mockSupplierDal = new MockSupplierDal().MockGet(null);
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetById(supplierId));
        }

        [Fact]
        public void GetById_WhenCalledWithId_ShouldReturnSupplier()
        {
            // Arrange
            int supplierId = 1;
            var mockSupplierDal = new MockSupplierDal().MockGet(new Supplier());
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.GetById(supplierId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetList_WhenCalledAll_ShouldReturnSuppliers()
        {
            // Arrange
            var mockSupplierDal = new MockSupplierDal().MockGetList(new List<Supplier>());
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetFixtures_WhenCalledFixtures_ShouldReturnFixtures()
        {
            // Arrange
            int supplierId = 1;
            var mockSupplierDal = new MockSupplierDal().MockGetFixtures(new List<Fixture>()).MockGet(new Supplier());
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.GetFixtures(supplierId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void Add_WhenAddedNewSupplier_ShouldAddAndReturnId()
        {
            // Arrange
            var supplierForAddDto = new SupplierForAddDto();
            var mockSupplierDal = new MockSupplierDal().MockAdd(new Supplier());
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.Add(supplierForAddDto);

            // Assert
            Assert.Equal(new int(), result.Data);
        }

        [Fact]
        public void Update_WhenUpdatedSupplier_ShouldUpdate()
        {
            // Arrange
            var supplierForUpdateDto = new SupplierForUpdateDto();
            var mockSupplierDal = new MockSupplierDal().MockUpdate().MockGet(new Supplier());
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            sut.Update(supplierForUpdateDto);

            // Assert
            mockSupplierDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Delete_WhenDeletedSupplier_ShouldUpdateEnableStatus()
        {
            // Arrange
            int supplierId = 1;
            var mockSupplierDal = new MockSupplierDal().MockUpdate().MockGet(new Supplier());
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            sut.Delete(supplierId);

            // Assert
            mockSupplierDal.VerifyUpdate(Times.Once());
        }
    }
}
