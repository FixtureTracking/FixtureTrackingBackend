using FixtureTracking.Entities.Dtos.Category;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.CategoryValidation
{
    public class CategoryForAddValidator : AbstractValidator<CategoryForAddDto>
    {
        public CategoryForAddValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).Length(2, 50);

            RuleFor(c => c.Description).MaximumLength(120);
        }
    }
}
