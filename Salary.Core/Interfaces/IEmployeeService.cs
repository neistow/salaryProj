using System.Collections.Generic;
using System.Threading.Tasks;
using Salary.Core.ApiModels;
using Salary.Core.Common;
using Salary.Core.DTO;

namespace Salary.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<Result<IEnumerable<EmployeeDto>>> GetEmployees();
        Task<Result<EmployeeDto>> GetEmployee(int employeeId);
        Task<Result<EmployeeSalaryDto>> GetEmployeeSalary(int employeeId, CurrencyType currency);
        Task<Result<EmployeeDto>> CreateEmployee(CreateEmployeeDto employeeDto);
        Task<Result<EmployeeDto>> EditEmployee(EditEmployeeDto employeeDto);
        Task<Result<object>> DeleteEmployee(int employeeId);
    }
}