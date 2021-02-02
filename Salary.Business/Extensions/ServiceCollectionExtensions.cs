using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Salary.Business.Services;
using Salary.Core.Interfaces;

namespace Salary.Business.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ISalaryConverterService, SalaryConverterService>();
        }
    }
}