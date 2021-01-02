using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks;
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
        public void GetById_WhenCalledWithNotExistsId_ShouldReturnNull()
        {
            // Arrange
            int supplierId = 111;
            var mockSupplierDal = new MockSupplierDal().MockGet(null);
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.GetById(supplierId);

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetById_WhenCalledWithId_ShouldReturnSupplier()
        {
            // Arrange
            int supplierId = 1;
            var supplier = new Supplier()
            {
                Id = supplierId
            };
            var mockSupplierDal = new MockSupplierDal().MockGet(supplier);
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.GetById(supplierId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetList_WhenThereIsNoSupplier_ShouldReturnEmptyList()
        {
            // Arrange
            var suppliers = new List<Supplier>();
            var mockSupplierDal = new MockSupplierDal().MockGetList(suppliers);
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public void GetList_WhenCalledAll_ShouldReturnSuppliers()
        {
            // Arrange
            var suppliers = new List<Supplier>() {
                new Supplier(),
                new Supplier()
            };
            var mockSupplierDal = new MockSupplierDal().MockGetList(suppliers);
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void Add_WhenAddedNewSupplier_ShouldAddedAndReturnId()
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
        public void Update_WhenUpdatedNotExistsSupplier_ShouldReturnErrorResult()
        {
            // Arrange
            var supplier = new Supplier();
            var mockSupplierDal = new MockSupplierDal().MockUpdate().MockGet(null);
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.Update(supplier);

            // Assert
            mockSupplierDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Update_WhenUpdatedSupplier_ShouldUpdate()
        {
            // Arrange
            var supplier = new Supplier();
            var mockSupplierDal = new MockSupplierDal().MockUpdate().MockGet(supplier);
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            sut.Update(supplier);

            // Assert
            mockSupplierDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Delete_WhenDeletedNotExistsSupplier_ShouldReturnErrorResult()
        {
            // Arrange
            int supplierId = 111;
            var mockSupplierDal = new MockSupplierDal().MockDelete().MockGet(null);
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            var result = sut.Delete(supplierId);

            // Assert
            mockSupplierDal.VerifyDelete(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Delete_WhenDeletedSupplier_ShouldDelete()
        {
            // Arrange
            int supplierId = 111;
            var mockSupplierDal = new MockSupplierDal().MockDelete().MockGet(new Supplier());
            var sut = new SupplierManager(mockSupplierDal.Object);

            // Act
            sut.Delete(supplierId);

            // Assert
            mockSupplierDal.VerifyDelete(Times.Once());
        }
    }
}
