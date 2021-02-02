using System;

namespace Salary.Core.DTO
{
    public record EditEmployeeDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DateEmployed { get; init; }
        public decimal Salary { get; init; }
        public int PositionId { get; init; }
    }
}