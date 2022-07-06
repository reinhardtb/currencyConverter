using Knab.Library.ApiClient.Models;

namespace Knab.Library.ApiClient.Interfaces
{
  public interface ICurrencyConversionApiClient
  {
    Task<IEnumerable<CurrencyConversion>> GetCurrencyConversionAsync(string currencyCode, string[] conversionCurrencies);
  }
}