using System.Collections.Generic;

namespace Salary.Core.Entities
{
    public class EmployeePosition
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}