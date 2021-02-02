using Salary.Core.DTO;
using Salary.Core.Entities;
using Salary.Core.Entities.Identity;

namespace Salary.Core
{
    public static class EntitiesExtensions
    {
        public static UserDto MapToDto(this User user)
        {
            return new UserDto
            {
                Email = user.Email,
                UserName = user.UserName
            };
        }

        public static EmployeeDto MapToDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                DateEmployed = employee.DateEmployed,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position.Name
            };
        }
    }
}