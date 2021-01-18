using FixtureTracking.Business.Concrete;
using FixtureTracking.Business.Tests.Mocks.Repositories;
using FixtureTracking.Core.Utilities.CustomExceptions;
using FixtureTracking.Entities.Concrete;
using FixtureTracking.Entities.Dtos.Category;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace FixtureTracking.Business.Tests.Services
{
    public class CategoryServiceTests
    {
        [Fact]
        public void GetById_WhenCalledNotExistsCategory_ShouldThrowObjectNotFoundException()
        {
            // Arrange 
            short categoryId = 111;
            var mockCategoryDal = new MockCategoryDal().MockGet(null);
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetById(categoryId));
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
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act
            var result = sut.GetById(categoryId);

            // Assert
            Assert.NotNull(result.Data);
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
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act
            var result = sut.GetList();

            // Assert
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public void GetFixtures_WhenNotExistsCategory_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            short categoryId = 111;
            var mockCategoryDal = new MockCategoryDal().MockGet(null);
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.GetFixtures(categoryId));
        }

        [Fact]
        public void GetFixtures_WhenCalledFixtures_ShouldReturnFixtures()
        {
            // Arrange
            short categoryId = 1;
            var mockCategoryDal = new MockCategoryDal().MockGetFixtures(new List<Fixture>()).MockGet(new Category());
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act
            var result = sut.GetFixtures(categoryId);

            // Assert
            Assert.NotNull(result.Data);
            Assert.True(result.Success);
        }

        [Fact]
        public void Add_WhenAddedNewCategory_ShouldAddAndReturnId()
        {
            // Arrange
            var categoryForAddDto = new CategoryForAddDto();
            var category = new Category();
            var mockCategoryDal = new MockCategoryDal().MockAdd(category);
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act
            var result = sut.Add(categoryForAddDto);

            // Assert
            Assert.Equal(new short(), result.Data);
        }

        [Fact]
        public void Update_WhenUpdatedNotExistsCategory_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            var categoryForUpdateDto = new CategoryForUpdateDto();
            var mockCategoryDal = new MockCategoryDal().MockUpdate().MockGet(null);
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Update(categoryForUpdateDto));
        }

        [Fact]
        public void Update_WhenUpdatedCategory_ShouldUpdate()
        {
            // Arrange
            var categoryForUpdateDto = new CategoryForUpdateDto();
            var mockCategoryDal = new MockCategoryDal().MockUpdate().MockGet(new Category());
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act
            sut.Update(categoryForUpdateDto);

            // Assert
            mockCategoryDal.VerifyUpdate(Times.Once());
        }

        [Fact]
        public void Deleted_WhenDeletedNotExistsCategory_ShouldThrowObjectNotFoundException()
        {
            // Arrange
            short categoryId = 111;
            var mockCategoryDal = new MockCategoryDal().MockUpdate().MockGet(null);
            var sut = new CategoryManager(mockCategoryDal.Object);


            // Act & Assert
            Assert.Throws<ObjectNotFoundException>(() => sut.Delete(categoryId));
        }

        [Fact]
        public void Deleted_WhenDeletedCategory_ShouldUpdateEnableStatus()
        {
            // Arrange
            short categoryId = 1;
            var mockCategoryDal = new MockCategoryDal().MockUpdate().MockGet(new Category());
            var sut = new CategoryManager(mockCategoryDal.Object);

            // Act
            sut.Delete(categoryId);

            // Assert
            mockCategoryDal.VerifyUpdate(Times.Once());
        }

    }
}
