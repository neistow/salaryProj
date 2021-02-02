using FluentValidation;
using Salary.Core.DTO;

namespace Salary.Api.Validators
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}