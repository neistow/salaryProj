using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Salary.Business.Services;
using Salary.Core.ApiModels;
using Salary.Core.Interfaces;
using Xunit;

namespace Salary.Tests
{
    public class SalaryConverterTests
    {
        private readonly ISalaryConverterService _converter;

        private readonly ICollection<ExchangeRate> _exchangeRates = new List<ExchangeRate>
        {
            new() {Ccy = CurrencyType.USD, BaseCcy = CurrencyType.UAH, Buy = 27.75000m, Sale = 28.16901m},
            new() {Ccy = CurrencyType.EUR, BaseCcy = CurrencyType.UAH, Buy = 33.50000m, Sale = 34.01361m},
            new() {Ccy = CurrencyType.RUR, BaseCcy = CurrencyType.UAH, Buy = 0.36000m, Sale = 0.40000m},
            new() {Ccy = CurrencyType.BTC, BaseCcy = CurrencyType.USD, Buy = 33034.5537m, Sale = 36511.8751m},
        };

        public SalaryConverterTests()
        {
            var currencyServiceMock = new Mock<ICurrencyService>();
            currencyServiceMock.Setup(e => e.GetExchangeRates())
                .ReturnsAsync(() => _exchangeRates);

            _converter = new SalaryConverterService(currencyServiceMock.Object);
        }

        [Theory]
        [InlineData(-1, CurrencyType.UAH)]
        [InlineData(-5, CurrencyType.USD)]
        [InlineData(-10, CurrencyType.RUR)]
        [InlineData(-100, CurrencyType.EUR)]
        public void ConvertSalary_NegativeValuePassed_ThrowsArgumentException(decimal salary,
            CurrencyType destCurrency)
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _converter.ConvertSalary(salary, destCurrency);
            });
        }

        [Theory]
        [InlineData("300", CurrencyType.UAH, "8325")]
        [InlineData("300", CurrencyType.USD, "300")]
        [InlineData("300", CurrencyType.BTC, "0,0082165048817227138246865881")]
        [InlineData("300", CurrencyType.RUR, "20812,5")]
        [InlineData("300", CurrencyType.EUR, "244,75496720283439482019109409")]
        public async Task ConvertSalary_WhenCalled_ReturnsCorrectlyConvertedSalary(string salary,
            CurrencyType destCurrency,
            string expected)
        {
            var parsedSalary = decimal.Parse(salary);
            var parsedExpected = decimal.Parse(expected);

            var result = await _converter.ConvertSalary(parsedSalary, destCurrency);
            Assert.Equal(parsedExpected, result);
        }

        [Theory]
        [InlineData(CurrencyType.UAH)]
        [InlineData(CurrencyType.USD)]
        [InlineData(CurrencyType.BTC)]
        [InlineData(CurrencyType.RUR)]
        [InlineData(CurrencyType.EUR)]
        public async Task ConvertSalary_WhenCalledWithZeroPassed_ReturnsZero(CurrencyType destCurrency)
        {
            var result = await _converter.ConvertSalary(0, destCurrency);
            Assert.Equal(0, result);
        }
    }
}