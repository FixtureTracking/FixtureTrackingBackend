using FixtureTracking.Entities.Dtos.Department;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.DepartmentValidations
{
    public class DepartmentForAddValidator : AbstractValidator<DepartmentForAddDto>
    {
        public DepartmentForAddValidator()
        {
            RuleFor(d => d.Name).NotEmpty();
            RuleFor(d => d.Name).Length(2, 50);

            RuleFor(d => d.Description).MaximumLength(120);
        }
    }
}
