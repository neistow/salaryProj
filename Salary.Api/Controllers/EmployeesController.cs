using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Salary.Api.Extensions;
using Salary.Core.ApiModels;
using Salary.Core.DTO;
using Salary.Core.Interfaces;

namespace Salary.Api.Controllers
{
    public class EmployeesController : ApiControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _employeeService.GetEmployees();

            return result.Succeeded
                ? Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpGet("{id:min(1)}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            var result = await _employeeService.GetEmployee(id);

            return result.Succeeded
                ? Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpGet("{employeeId:min(1)}/salary")]
        public async Task<IActionResult> GetEmployeeSalary([FromRoute] int employeeId,
            [FromQuery] CurrencyType currency = CurrencyType.USD)
        {
            var result = await _employeeService.GetEmployeeSalary(employeeId, currency);

            return result.Succeeded
                ? Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto dto)
        {
            var result = await _employeeService.CreateEmployee(dto);

            return result.Succeeded
                ? Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody] EditEmployeeDto dto)
        {
            var result = await _employeeService.EditEmployee(dto);

            return result.Succeeded
                ? Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var result = await _employeeService.DeleteEmployee(id);

            return result.Succeeded
                ? Ok(result.ToApiResponse())
                : BadRequest(result.ToApiError());
        }
    }
}