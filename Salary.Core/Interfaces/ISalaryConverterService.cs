using System.Threading.Tasks;
using Salary.Core.ApiModels;

namespace Salary.Core.Interfaces
{
    public interface ISalaryConverterService
    {
        /// <summary>
        /// Converts salary in dollars into the destination currency salary
        /// </summary>
        /// <param name="salary">Salary in dollars</param>
        /// <param name="destCurrency">Result currency</param>
        /// <returns>Converted salary</returns>
        Task<decimal> ConvertSalary(decimal salary, CurrencyType destCurrency);
    }
}