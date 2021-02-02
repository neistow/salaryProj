using Salary.Core.ApiModels;

namespace Salary.Core.DTO
{
    public record EmployeeSalaryDto
    {
        public CurrencyType CurrencyType { get; init; }
        public decimal Amount { get; init; }
    }
}