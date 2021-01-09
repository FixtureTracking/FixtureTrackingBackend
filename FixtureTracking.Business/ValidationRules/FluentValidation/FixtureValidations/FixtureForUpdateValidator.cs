using FixtureTracking.Entities.Dtos.Fixture;
using FluentValidation;
using System;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.FixtureValidations
{
    public class FixtureForUpdateValidator : AbstractValidator<FixtureForUpdateDto>
    {
        public FixtureForUpdateValidator()
        {
            RuleFor(f => f.Id).NotEmpty();

            RuleFor(f => f.CategoryId).NotEmpty();

            RuleFor(f => f.DatePurchase).NotEmpty();
            RuleFor(f => f.DatePurchase).LessThan(DateTime.Now);

            RuleFor(f => f.DateWarranty).NotEmpty();
            RuleFor(f => f.DateWarranty).GreaterThan(f => f.DatePurchase);

            RuleFor(f => f.Description).MaximumLength(250);

            RuleFor(f => f.Name).NotEmpty();
            RuleFor(f => f.Name).Length(5, 50);

            RuleFor(f => f.PictureUrl).NotEmpty();
            RuleFor(f => f.PictureUrl).MaximumLength(500);

            RuleFor(f => f.Price).NotEmpty();
            RuleFor(f => f.Price).GreaterThanOrEqualTo(1M);

            RuleFor(f => f.SupplierId).NotEmpty();
        }
    }
}
