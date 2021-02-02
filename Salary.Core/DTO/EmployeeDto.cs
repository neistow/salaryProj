using System;

namespace Salary.Core.DTO
{
    public record EmployeeDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DateEmployed { get; init; }

        public string Position { get; init; }
    }
}