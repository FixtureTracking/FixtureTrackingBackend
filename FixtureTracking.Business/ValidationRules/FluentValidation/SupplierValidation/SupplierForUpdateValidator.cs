using FixtureTracking.Entities.Dtos.Supplier;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.SupplierValidation
{
    public class SupplierForUpdateValidator : AbstractValidator<SupplierForUpdateDto>
    {
        public SupplierForUpdateValidator()
        {
            RuleFor(s => s.Id).NotEmpty();

            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Name).Length(5, 50);

            RuleFor(s => s.Description).MaximumLength(120);
        }
    }
}
