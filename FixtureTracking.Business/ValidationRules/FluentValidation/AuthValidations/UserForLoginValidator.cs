using FixtureTracking.Entities.Dtos.User;
using FluentValidation;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.AuthValidations
{
    public class UserForLoginValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginValidator()
        {
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).EmailAddress();
            RuleFor(u => u.Email).MaximumLength(50);

            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).MaximumLength(20);
        }
    }
}
