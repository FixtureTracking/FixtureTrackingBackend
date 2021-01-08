using FixtureTracking.Entities.Dtos.Department;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.DepartmentValidations
{
    public class DepartmentForUpdateClaimsValidator : AbstractValidator<DepartmentForUpdateClaimDto>
    {
        public DepartmentForUpdateClaimsValidator()
        {
            RuleFor(d => d.Id).NotEmpty();

            RuleFor(d => d.OperationClaimNames).NotNull();
        }
    }
}
