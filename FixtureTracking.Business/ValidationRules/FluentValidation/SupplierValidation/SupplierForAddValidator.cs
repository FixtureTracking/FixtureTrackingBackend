using FixtureTracking.Entities.Dtos.Supplier;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.SupplierValidation
{
    public class SupplierForAddValidator : AbstractValidator<SupplierForAddDto>
    {
        public SupplierForAddValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Name).Length(5, 50);

            RuleFor(s => s.Description).MaximumLength(120);
        }
    }
}
