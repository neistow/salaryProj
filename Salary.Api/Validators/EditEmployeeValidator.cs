using FluentValidation;
using Salary.Core.DTO;

namespace Salary.Api.Validators
{
    public class EditEmployeeValidator : AbstractValidator<EditEmployeeDto>
    {
        public EditEmployeeValidator()
        {
            RuleFor(e => e.Id).GreaterThanOrEqualTo(1);
            RuleFor(e => e.Salary).GreaterThan(0);
            RuleFor(e => e.FirstName).MinimumLength(2).MaximumLength(256);
            RuleFor(e => e.LastName).MinimumLength(2).MaximumLength(256);
            RuleFor(e => e.PositionId).GreaterThanOrEqualTo(1);
        }
    }
}