using FixtureTracking.Business.Constants;
using FixtureTracking.Business.ValidationRules.FluentValidation.AuthValidations;
using FixtureTracking.Business.ValidationRules.FluentValidation.CategoryValidations;
using FixtureTracking.Business.ValidationRules.FluentValidation.DebitValidations;
using FixtureTracking.Business.ValidationRules.FluentValidation.DepartmentValidations;
using FixtureTracking.Business.ValidationRules.FluentValidation.FixtureValidations;
using FixtureTracking.Business.ValidationRules.FluentValidation.SupplierValidations;
using FixtureTracking.Entities.Dtos.Category;
using FixtureTracking.Entities.Dtos.Debit;
using FixtureTracking.Entities.Dtos.Department;
using FixtureTracking.Entities.Dtos.Fixture;
using FixtureTracking.Entities.Dtos.Supplier;
using FixtureTracking.Entities.Dtos.User;
using FluentValidation.TestHelper;
using System;
using Xunit;

namespace FixtureTracking.Business.Tests.ValidationRules.FluentValidation
{
    public class ModelValidatorTests
    {
        [Fact]
        public void UserForLoginValidator_TrueStory()
        {
            // Arrange
            var model = new UserForLoginDto()
            {
                Email = "email@test.com",
                Password = "password"
            };
            var sut = new UserForLoginValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void UserForRegisterValidator_WhenBirthDateExceedsMaxValue_ShouldHaveError()
        {
            // Arrange
            var model = new UserForRegisterDto()
            {
                BirthDate = DateTime.Now.AddYears(-2)
            };
            var message = Messages.BirthDateIsNotValid_LessThan(DateTime.Now.AddYears(-18));
            var sut = new UserForRegisterValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.BirthDate).WithErrorMessage(message);
        }

        [Fact]
        public void UserForRegisterValidator_WhenBirthDateExceedsMinValue_ShouldHaveError()
        {
            // Arrange
            var model = new UserForRegisterDto()
            {
                BirthDate = DateTime.Now.AddYears(-110)
            };
            var message = Messages.BirthDateIsNotValid_GreaterThan(DateTime.Now.AddYears(-100));
            var sut = new UserForRegisterValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.BirthDate).WithErrorMessage(message);
        }

        [Fact]
        public void UserForRegisterValidator_WhenEmailRegexDoesNotMatches_ShouldHaveError()
        {
            // Arrange
            var model = new UserForRegisterDto()
            {
                Email = "email@test.c"
            };
            var message = Messages.EmailIsNotValid;
            var sut = new UserForRegisterValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.Email).WithErrorMessage(message);
        }

        [Fact]
        public void UserForRegisterValidator_WhenUsernameRegexDoesNotMatches_ShouldHaveError()
        {
            // Arrange
            var model = new UserForRegisterDto()
            {
                Username = "Test_user"
            };
            var message = Messages.UsernameIsNotValid;
            var sut = new UserForRegisterValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.Username).WithErrorMessage(message);
        }

        [Fact]
        public void UserForRegisterValidator_WhenPasswordRegexDoesNotMatches_ShouldHaveError()
        {
            // Arrange
            var model = new UserForRegisterDto()
            {
                Password = "pAs$word"
            };
            var message = Messages.PasswordIsNotValid;
            var sut = new UserForRegisterValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.Password).WithErrorMessage(message);
        }

        [Fact]
        public void UserForRegisterValidator_TrueStory()
        {
            // Arrange
            var model = new UserForRegisterDto()
            {
                BirthDate = DateTime.Now.AddYears(-30),
                DepartmentId = 1,
                Email = "email@test.com",
                FirstName = "Test",
                LastName = "User",
                Password = "pas$W0rd",
                Username = "test.user"
            };
            var sut = new UserForRegisterValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CategoryForAddValidator_TrueStory()
        {
            // Arrange
            var model = new CategoryForAddDto()
            {
                Description = "Desc Category T",
                Name = "Category T"
            };
            var sut = new CategoryForAddValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CategoryForUpdateValidator_TrueStory()
        {
            // Arrange
            var model = new CategoryForUpdateDto()
            {
                Id = 1,
                Description = "Desc Category T",
                Name = "Category T"
            };
            var sut = new CategoryForUpdateValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void DebitForAddValidator_TrueStory()
        {
            // Arrange
            var model = new DebitForAddDto()
            {
                DateDebit = DateTime.Now.AddDays(-1),
                Description = "Desc Debit",
                FixtureId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };
            var sut = new DebitForAddValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void DebitForUpdateValidator_TrueStory()
        {
            // Arrange
            var model = new DebitForUpdateDto()
            {
                Id = Guid.NewGuid(),
                DateDebit = DateTime.Now.AddDays(-1),
                Description = "Desc Debit",
                FixtureId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };
            var sut = new DebitForUpdateValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void DepartmentForAddValidator_TrueStory()
        {
            // Arrange
            var model = new DepartmentForAddDto()
            {
                Description = "Desc Department T",
                Name = "Department T"
            };
            var sut = new DepartmentForAddValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void DepartmentForUpdateValidator_TrueStory()
        {
            // Arrange
            var model = new DepartmentForUpdateDto()
            {
                Id = 1,
                Description = "Desc Department T",
                Name = "Department T"
            };
            var sut = new DepartmentForUpdateValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void DepartmentForUpdateClaimsValidator_TrueStory()
        {
            // Arrange
            var model = new DepartmentForUpdateClaimDto()
            {
                Id = 1,
                OperationClaimNames = new string[] { "Department.Add", "Department.Update" }
            };
            var sut = new DepartmentForUpdateClaimsValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void FixtureForAddValidator_WhenDatePurchaseGreaterThanDateWarranty_ShouldHaveError()
        {
            // Arrange
            var model = new FixtureForAddDto()
            {
                DatePurchase = DateTime.Now.AddMinutes(-1),
                DateWarranty = DateTime.Now.AddHours(-1),
            };
            var sut = new FixtureForAddValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.DateWarranty);
        }

        [Fact]
        public void FixtureForAddValidator_TrueStory()
        {
            // Arrange
            var model = new FixtureForAddDto()
            {
                CategoryId = 1,
                DatePurchase = DateTime.Now.AddMinutes(-2),
                DateWarranty = DateTime.Now.AddYears(2),
                Description = "Desc Fixture T",
                Name = "Fixture T",
                PictureUrl = "picture.lk",
                Price = 10000M,
                SupplierId = 1
            };
            var sut = new FixtureForAddValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void FixtureForUpdateValidator_WhenPriceLessThanOne_ShouldHaveError()
        {
            // Arrange
            var model = new FixtureForUpdateDto()
            {
                Price = 0.8M,
            };
            var sut = new FixtureForUpdateValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(m => m.Price);
        }

        [Fact]
        public void FixtureForUpdateValidator_TrueStory()
        {
            // Arrange
            var model = new FixtureForUpdateDto()
            {
                Id = Guid.NewGuid(),
                CategoryId = 1,
                DatePurchase = DateTime.Now.AddMinutes(-2),
                DateWarranty = DateTime.Now.AddYears(2),
                Description = "Desc Fixture T",
                Name = "Fixture T",
                PictureUrl = "picture.lk",
                Price = 10000M,
                SupplierId = 1
            };
            var sut = new FixtureForUpdateValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void SupplierForAddValidator_TrueStory()
        {
            // Arrange
            var model = new SupplierForAddDto()
            {
                Description = "Desc Supplier T",
                Name = "Supplier T"
            };
            var sut = new SupplierForAddValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void SupplierForUpdateValidator_TrueStory()
        {
            // Arrange
            var model = new SupplierForUpdateDto()
            {
                Id = 1,
                Description = "Desc Supplier T",
                Name = "Supplier T"
            };
            var sut = new SupplierForUpdateValidator();

            // Act
            var result = sut.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
