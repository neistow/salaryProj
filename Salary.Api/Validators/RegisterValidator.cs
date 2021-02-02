using FluentValidation;
using Salary.Core.DTO;

namespace Salary.Api.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(r => r.UserName).MinimumLength(5).MaximumLength(32);
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})")
                .WithMessage(
                    "Password should have: 1 lower character, 1 upper character, 1 numeric character, 1 special symbol and be at least 8 characters long");
        }
    }
}