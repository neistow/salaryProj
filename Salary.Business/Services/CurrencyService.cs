using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Salary.Core.ApiModels;
using Salary.Core.Configurations;
using Salary.Core.Interfaces;

namespace Salary.Business.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly HttpClient _httpClient;
        private readonly CurrencyApiConfig _config;

        public CurrencyService(IHttpClientFactory clientFactory, IOptions<CurrencyApiConfig> options)
        {
            _config = options.Value;

            _httpClient = clientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(_config.BaseUrl);
        }

        public async Task<ICollection<ExchangeRate>> GetExchangeRates()
        {
            var exchangeRates = await _httpClient.GetFromJsonAsync<List<ExchangeRate>>(_config.ExchangeRateEndpointUrl,
                new JsonSerializerOptions
                {
                    Converters = {new JsonStringEnumConverter()},
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                });

            return exchangeRates;
        }
    }
}