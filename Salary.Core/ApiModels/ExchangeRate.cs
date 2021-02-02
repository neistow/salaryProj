using System.Text.Json.Serialization;

namespace Salary.Core.ApiModels
{
    public class ExchangeRate
    {
        [JsonPropertyName("ccy")] public CurrencyType Ccy { get; set; }
        [JsonPropertyName("base_ccy")] public CurrencyType BaseCcy { get; set; }
        [JsonPropertyName("buy")] public decimal Buy { get; set; }
        [JsonPropertyName("sale")] public decimal Sale { get; set; }
    }
}