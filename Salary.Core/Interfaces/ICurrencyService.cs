using System.Collections.Generic;
using System.Threading.Tasks;
using Salary.Core.ApiModels;

namespace Salary.Core.Interfaces
{
    public interface ICurrencyService
    {
        Task<ICollection<ExchangeRate>> GetExchangeRates();
    }
}