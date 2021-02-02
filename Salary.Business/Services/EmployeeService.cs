using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Salary.Core;
using Salary.Core.ApiModels;
using Salary.Core.Common;
using Salary.Core.DTO;
using Salary.Core.Entities;
using Salary.Core.Interfaces;
using Salary.Data;
using Salary.Data.Extensions;

namespace Salary.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly SalaryDbContext _salaryDbContext;
        private readonly ISalaryConverterService _converter;

        public EmployeeService(SalaryDbContext salaryDbContext, ISalaryConverterService converter)
        {
            _salaryDbContext = salaryDbContext;
            _converter = converter;
        }

        public async Task<Result<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            var employees = await _salaryDbContext.Employees
                .IncludePosition()
                .AsNoTracking()
                .ToListAsync();

            return Result<IEnumerable<EmployeeDto>>.Success(employees.Select(e => e.MapToDto()));
        }

        public async Task<Result<EmployeeDto>> GetEmployee(int employeeId)
        {
            var employee = await _salaryDbContext.Employees
                .IncludePosition()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == employeeId);

            return employee == null
                ? Result<EmployeeDto>.Failure(nameof(employeeId), "Employee doesn't exist")
                : Result<EmployeeDto>.Success(employee.MapToDto());
        }

        public async Task<Result<EmployeeSalaryDto>> GetEmployeeSalary(int employeeId, CurrencyType currency)
        {
            var employee = await _salaryDbContext.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return Result<EmployeeSalaryDto>.Failure(nameof(employeeId), "Employee doesn't exist");
            }

            var convertedSalary = await _converter.ConvertSalary(employee.Salary, currency);
            return Result<EmployeeSalaryDto>.Success(new EmployeeSalaryDto
            {
                Amount = convertedSalary,
                CurrencyType = currency
            });
        }

        public async Task<Result<EmployeeDto>> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            var position = await _salaryDbContext.EmployeePositions.FindAsync(employeeDto.PositionId);
            if (position == null)
            {
                return Result<EmployeeDto>.Failure(nameof(employeeDto.PositionId), "Position doesn't exist");
            }

            var employee = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Salary = employeeDto.Salary,
                Position = position,
                DateEmployed = DateTime.UtcNow
            };
            await _salaryDbContext.Employees.AddAsync(employee);
            await _salaryDbContext.SaveChangesAsync();

            return Result<EmployeeDto>.Success(employee.MapToDto());
        }

        public async Task<Result<EmployeeDto>> EditEmployee(EditEmployeeDto employeeDto)
        {
            var employee = await _salaryDbContext.Employees.FindAsync(employeeDto.Id);
            if (employee == null)
            {
                return Result<EmployeeDto>.Failure(nameof(employee.Id), "Employee doesn't exist");
            }

            var position = await _salaryDbContext.EmployeePositions.FindAsync(employeeDto.PositionId);
            if (position == null)
            {
                return Result<EmployeeDto>.Failure(nameof(employeeDto.PositionId), "Position doesn't exist");
            }

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Salary = employeeDto.Salary;
            employee.DateEmployed = employeeDto.DateEmployed;
            employee.Position = position;

            await _salaryDbContext.SaveChangesAsync();

            return Result<EmployeeDto>.Success(employee.MapToDto());
        }

        public async Task<Result<object>> DeleteEmployee(int employeeId)
        {
            var employee = await _salaryDbContext.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return Result<object>.Failure(nameof(employeeId), "Employee doesn't exist");
            }

            _salaryDbContext.Employees.Remove(employee);
            await _salaryDbContext.SaveChangesAsync();

            return Result<object>.Success();
        }
    }
}