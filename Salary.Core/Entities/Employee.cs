using System;

namespace Salary.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateEmployed { get; set; }
        public decimal Salary { get; set; }

        public int PositionId { get; set; }
        public EmployeePosition Position { get; set; }
    }
}