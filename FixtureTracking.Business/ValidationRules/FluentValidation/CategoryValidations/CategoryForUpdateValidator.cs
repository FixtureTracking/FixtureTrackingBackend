using FixtureTracking.Entities.Dtos.Category;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.CategoryValidations
{
    public class CategoryForUpdateValidator : AbstractValidator<CategoryForUpdateDto>
    {
        public CategoryForUpdateValidator()
        {
            RuleFor(c => c.Id).NotEmpty();

            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).Length(5, 50);

            RuleFor(c => c.Description).MaximumLength(120);
        }
    }
}
