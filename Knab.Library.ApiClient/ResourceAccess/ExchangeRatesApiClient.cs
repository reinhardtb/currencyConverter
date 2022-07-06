using Flurl;
using Flurl.Http;
using Knab.Library.ApiClient.Interfaces;
using Knab.Library.ApiClient.Models;
using Knab.Library.ApiClient.Models.ExchangeRatesApi;
using Newtonsoft.Json;

namespace Knab.Library.ApiClient.ResourceAccess
{
  /*
  API request example
  https://api.exchangeratesapi.io/v1/2013-12-24
    ? access_key = API_KEY
    & base = GBP
    & symbols = USD,CAD,EUR

  or from the APILayer ... ?
  https://api.apilayer.com/exchangerates_data/latest?symbols={symbols}&base={base}

   */

  public class ExchangeRatesApiClient : ICurrencyConversionApiClient
  {
    private readonly IApiClientConfiguration _configuration;

    private const string RESPONSERECEIVEDISNULL = "The response received from the server could not be parsed to the given type.";
    private const string RESPONSERECEIVEDISUNSUCCESSFUL = "The request to the currency conversion provider was unsuccessful.";
    public ExchangeRatesApiClient(IApiClientConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task<IEnumerable<CurrencyConversion>> GetCurrencyConversionAsync(string currencyCode, string[] conversionCurrencies)
    {
      try
      {
        var conversionsResponse = await _configuration.BaseUri
          .AppendPathSegment("latest")
          .WithHeader("apikey", _configuration.ApiKey)
          .SetQueryParam("base", currencyCode)
          .SetQueryParam("symbols", conversionCurrencies.Aggregate((a, b) => $"{a},{b}"))
          .GetJsonAsync<LatestRates>()
          .ConfigureAwait(false);

        if (conversionsResponse == null)
          throw new ArgumentException(RESPONSERECEIVEDISNULL);
        else if (!conversionsResponse.Success)
          throw new ArgumentException(RESPONSERECEIVEDISUNSUCCESSFUL);

        return conversionsResponse.ConversionRates.Select(rate => new CurrencyConversion { CurrencyCode = rate.Key, ConversionRate = rate.Value });
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}