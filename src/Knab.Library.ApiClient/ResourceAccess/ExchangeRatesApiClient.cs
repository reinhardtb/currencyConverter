using Flurl;
using Flurl.Http;
using Knab.Library.ApiClient.Interfaces;
using Knab.Library.ApiClient.Models;
using Knab.Library.ApiClient.Models.ExchangeRatesApi;

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
    private readonly ApiClientConfiguration _configuration;

    private const string RESPONSERECEIVEDISNULL = "The response received from the server could not be parsed to the given type.";
    private const string RESPONSERECEIVEDISUNSUCCESSFUL = "The request to the currency conversion provider was unsuccessful.";
    private const string RESPONSERECEIVEDCONTAINSNOCONVERSIONS = "The request to the currency conversion provider did not return any conversion rates.";
    private const string INVALIDURICONFIGURATION = "The API URI provided through configuration is invalid.";
    private const string INVALIDAPIKEYCONFIGURATION = "The API key provided through configuration can not authorize the request.";
    private const string INVALIDAPIREQUEST = "The request failed. This could be due to the input currency code not being found.";
    private const int HTTPUNAUTHORIZEDSTATUSCODE = 401;
    private const int HTTPBADREQUESTSTATUSCODE = 400;

    public ExchangeRatesApiClient(ApiClientConfiguration configuration)
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
          throw new HttpRequestException(RESPONSERECEIVEDISUNSUCCESSFUL);

        if (conversionsResponse.ConversionRates == null ||
          !conversionsResponse.ConversionRates.Any())
          throw new ArgumentException(RESPONSERECEIVEDCONTAINSNOCONVERSIONS);

        return conversionsResponse.ConversionRates.Select(rate => new CurrencyConversion { CurrencyCode = rate.Key, ConversionRate = rate.Value });
      }
      catch (UriFormatException ufex)
      {
        throw new ArgumentException(INVALIDURICONFIGURATION, ufex);
      }
      catch (FlurlHttpException fhex)
      {
        if (fhex.StatusCode == HTTPUNAUTHORIZEDSTATUSCODE)
          throw new ArgumentException(INVALIDAPIKEYCONFIGURATION, fhex);
        else if (fhex.StatusCode == HTTPBADREQUESTSTATUSCODE)
          throw new ArgumentException(INVALIDAPIREQUEST, fhex);
        else
          throw;
      }
    }
  }
}