using Newtonsoft.Json;

namespace Knab.Library.ApiClient.Models.ExchangeRatesApi
{
  internal class LatestRates
  {
    [JsonPropertyAttribute("success")]
    internal bool Success { get; set; }
    [JsonPropertyAttribute("timestamp")]
    internal long UnixTimestampTicks { get; set; }
    [JsonPropertyAttribute("base")]
    internal string? BaseCurrencyCode { get; set; }
    [JsonPropertyAttribute("date")]
    internal string? Date { get; set; }
    [JsonPropertyAttribute("rates")]
    internal Dictionary<string, double>? ConversionRates { get; set; }
  }
}