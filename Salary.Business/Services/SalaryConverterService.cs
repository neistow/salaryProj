using System;
using System.Linq;
using System.Threading.Tasks;
using Salary.Core.ApiModels;
using Salary.Core.Interfaces;
using Salary.Data;

namespace Salary.Business.Services
{
    public class SalaryConverterService : ISalaryConverterService
    {
        private readonly ICurrencyService _currencyService;

        public SalaryConverterService(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public async Task<decimal> ConvertSalary(decimal salary, CurrencyType destCurrency)
        {
            // For now this method has 4 different paths for different salary conversions.
            // Since more currency types could be added in future this method should
            // be refactored (e.g here we can apply Strategy pattern).
            // But to avoid overengineering and unnecessary complexity this method was kept as is. 

            if (salary < 0)
            {
                throw new ArgumentException("Salary couldn't be negative");
            }

            if (destCurrency == CurrencyType.USD)
            {
                return salary;
            }

            var exchangeRates = await _currencyService.GetExchangeRates();

            if (destCurrency == CurrencyType.UAH)
            {
                return exchangeRates.First(er => er.Ccy == CurrencyType.USD).Buy * salary;
            }

            if (destCurrency == CurrencyType.BTC)
            {
                return salary / exchangeRates.First(er => er.Ccy == CurrencyType.BTC).Sale;
            }

            var amountInUah = exchangeRates.First(er => er.Ccy == CurrencyType.USD).Buy * salary;
            return amountInUah / exchangeRates.First(er => er.Ccy == destCurrency).Sale;
        }
    }
}