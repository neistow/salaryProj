namespace Salary.Core.Configurations
{
    public record CurrencyApiConfig
    {
        public string BaseUrl { get; init; }
        public string ExchangeRateEndpointUrl { get; init; }
    }
}