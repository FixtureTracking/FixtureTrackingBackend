using FixtureTracking.Business.Constants;
using FixtureTracking.Entities.Dtos.User;
using FluentValidation;
using System;

namespace FixtureTracking.Business.ValidationRules.FluentValidation.AuthValidations
{
    public class UserForRegisterValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterValidator()
        {
            var dateNow = DateTime.Now;
            var dateLessThan = dateNow.AddYears(-18);
            var dateGreaterThan = dateNow.AddYears(-100);

            RuleFor(u => u.BirthDate).NotEmpty();
            RuleFor(u => u.BirthDate).LessThan(dateLessThan).WithMessage(Messages.BirthDateIsNotValid_LessThan(dateLessThan));
            RuleFor(u => u.BirthDate).GreaterThan(dateGreaterThan).WithMessage(Messages.BirthDateIsNotValid_GreaterThan(dateGreaterThan));

            RuleFor(u => u.DepartmentId).NotEmpty();

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).Matches(RegexExpressions.EmailRegex).WithMessage(Messages.EmailIsNotValid);
            RuleFor(u => u.Email).MaximumLength(50);

            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.FirstName).Length(2, 50);

            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.LastName).Length(2, 50);

            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).Length(6, 20);
            RuleFor(u => u.Password).Matches(RegexExpressions.PasswordRegex).WithMessage(Messages.PasswordIsNotValid);

            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Username).Length(3, 20);
            RuleFor(u => u.Username).Matches(RegexExpressions.UsernameRegex).WithMessage(Messages.UsernameIsNotValid);
        }
    }
}
