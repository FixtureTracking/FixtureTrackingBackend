using FixtureTracking.Entities.Dtos.Category;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.CategoryValidations
{
    public class CategoryForAddValidator : AbstractValidator<CategoryForAddDto>
    {
        public CategoryForAddValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).Length(5, 50);

            RuleFor(c => c.Description).MaximumLength(120);
        }
    }
}
