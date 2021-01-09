using FixtureTracking.Entities.Dtos.Debit;
using FluentValidation;
using System;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.DebitValidations
{
    public class DebitForAddValidator : AbstractValidator<DebitForAddDto>
    {
        public DebitForAddValidator()
        {
            RuleFor(d => d.DateDebit).NotEmpty();
            RuleFor(d => d.DateDebit).LessThan(DateTime.Now);

            RuleFor(d => d.Description).MaximumLength(250);

            RuleFor(d => d.FixtureId).NotEmpty();

            RuleFor(d => d.UserId).NotEmpty();
        }
    }
}
