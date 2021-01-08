using FixtureTracking.Entities.Dtos.Department;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.DepartmentValidations
{
    public class DepartmentForUpdateValidator : AbstractValidator<DepartmentForUpdateDto>
    {
        public DepartmentForUpdateValidator()
        {
            RuleFor(d => d.Id).NotEmpty();

            RuleFor(d => d.Name).NotEmpty();
            RuleFor(d => d.Name).Length(2, 50);

            RuleFor(d => d.Description).MaximumLength(120);
        }
    }
}
