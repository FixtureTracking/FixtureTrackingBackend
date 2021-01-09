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
            RuleFor(u => u.BirthDate).LessThan(dateLessThan)
                .WithMessage($"'Birth Date' must be less than '{dateLessThan.ToShortDateString()}'.");
            RuleFor(u => u.BirthDate).GreaterThan(dateGreaterThan)
                .WithMessage($"'Birth Date' must be greater than '{dateGreaterThan.ToShortDateString()}'.");

            RuleFor(u => u.DepartmentId).NotEmpty();

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
                .WithMessage("'Email' is not a valid email address.");
            RuleFor(u => u.Email).MaximumLength(50);

            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.FirstName).Length(2, 50);

            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.LastName).Length(2, 50);

            RuleFor(u => u.Password).NotEmpty();
            RuleFor(u => u.Password).Length(6, 20);
            RuleFor(u => u.Password).Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{6,20}$")
                .WithMessage("'Password' is not a valid password. 'Password' must be 6-20 characters, include at least one lowercase letter, one uppercase letter, a special character and a digit.");

            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Username).Length(3, 20);
            RuleFor(u => u.Username).Matches(@"^(?=[a-z0-9.]{3,20}$)(?!.*[.]{2})[^.].*[^.]$")
                .WithMessage("'Username' is not a valid username. 'Username' must be 3-20 characters and contain only lowercase letters and dots.");
        }
    }
}
