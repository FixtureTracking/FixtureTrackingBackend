using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Business.Tests.Mocks.Services;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Category;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class CategoryServiceTests
    {
        [Fact]
        public void GetById_WhenCalledWithNotExistId_ShouldReturnNull()
        {
            // Arrange
            short categoryId = 111;
            var mockCategoryDal = new MockCategoryDal().MockGet(null);
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            var result = sut.GetById(categoryId);

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetById_WhenCalledWithId_ShouldReturnCategory()
        {
            // Arrange
            short categoryId = 1;
            Category category = new Category()
            {
                Id = categoryId
            };
            var mockCategoryDal = new MockCategoryDal().MockGet(category);
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            var result = sut.GetById(categoryId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void GetList_WhenThereIsNoCategory_ShouldReturnEmptyList()
        {
            // Arrange
            var categories = new List<Category>();
            var mockCategoryDal = new MockCategoryDal().MockGetList(categories);
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.Empty(result.Data);
        }

        [Fact]
        public void GetList_WhenCalledAll_ShouldReturnCategories()
        {
            // Arrange
            var categories = new List<Category>()
            {
                new Category(),
                new Category()
            };
            var mockCategoryDal = new MockCategoryDal().MockGetList(categories);
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetFixtures_WhenNotExistsCategory_ShouldReturnErrorResult()
        {
            // Arrange
            short categoryId = 111;
            var mockCategoryDal = new MockCategoryDal().MockGet(null);
            var mockFixtureService = new MockFixtureService();
            var sut = new CategoryManager(mockCategoryDal.Object, mockFixtureService.Object);

            // Act
            var result = sut.GetFixtures(categoryId);

            // Assert
            Assert.False(result.Success);
        }

        [Fact]
        public void GetFixtures_WhenCalledFixtures_ShouldReturnFixtures()
        {
            // Arrange
            short categoryId = 1;
            var mockCategoryDal = new MockCategoryDal().MockGet(new Category());
            var mockFixtureService = new MockFixtureService().MockGetListByCategoryId(new List<Fixture>());
            var sut = new CategoryManager(mockCategoryDal.Object, mockFixtureService.Object);

            // Act
            var result = sut.GetFixtures(categoryId);

            // Assert
            Assert.NotNull(result.Data);
        }

        [Fact]
        public void Add_WhenAddedNewCategory_ShouldAddAndReturnId()
        {
            // Arrange
            var categoryForAddDto = new CategoryForAddDto();
            var category = new Category();
            var mockCategoryDal = new MockCategoryDal().MockAdd(category);
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            var result = sut.Add(categoryForAddDto);

            // Assert
            Assert.Equal(new short(), result.Data);
        }

        [Fact]
        public void Update_WhenUpdatedNotExistsCategory_ShouldReturnErrorResult()
        {
            // Arrange
            var categoryForUpdateDto = new CategoryForUpdateDto();
            var mockCategoryDal = new MockCategoryDal().MockUpdate().MockGet(null);
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            var result = sut.Update(categoryForUpdateDto);

            // Assert
            mockCategoryDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Update_WhenUpdatedCategory_ShouldUpdate()
        {
            // Arrange
            var categoryForUpdateDto = new CategoryForUpdateDto();
            var mockCategoryDal = new MockCategoryDal().MockUpdate().MockGet(new Category());
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            sut.Update(categoryForUpdateDto);

            // Assert
            mockCategoryDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Deleted_WhenDeletedNotExistsCategory_ShouldReturnErrorResult()
        {
            // Arrange
            short categoryId = 111;
            var mockCategoryDal = new MockCategoryDal().MockUpdate().MockGet(null);
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            var result = sut.Delete(categoryId);

            // Assert
            mockCategoryDal.VerifyUpdate(Times.Never());
            Assert.False(result.Success);
        }

        [Fact]
        public void Deleted_WhenDeletedCategory_ShouldUpdateEnableStatus()
        {
            // Arrange
            short categoryId = 1;
            var mockCategoryDal = new MockCategoryDal().MockUpdate().MockGet(new Category());
            var sut = new CategoryManager(mockCategoryDal.Object, null);

            // Act
            sut.Delete(categoryId);

            // Assert
            mockCategoryDal.VerifyUpdate(Times.Once());
        }

    }
}
