using Flurl.Http;
using Knab.Library.ApiClient.Interfaces;
using Knab.Library.ApiClient.Models;
using System.Text.Json;

namespace Knab.Library.ApiClient.ResourceAccess
{
  public class ExchangeRatesApiIoApiClient : ICurrencyConversionApiClient
  {
    private readonly IApiClientConfiguration _configuration;
    private readonly IFlurlClient _httpClient;

    private const string RESPONSERECEIVEDISNULL = "The response received from the server could not be parsed to the given type.";
    private const string AUTHENTICATIONHEADERNAME = "X-CMC_PRO_API_KEY";
    private const string CURRENCYCONVERSIONURIPATH = "";

    public ExchangeRatesApiIoApiClient(IApiClientConfiguration configuration, IFlurlClient httpClient)
    {
      _configuration = configuration;
      _httpClient = httpClient;

      _httpClient.BaseUrl = _configuration.BaseUri;
    }

    public async Task<IEnumerable<CurrencyConversion>> GetCurrencyConversionAsync(string currencyCode, string[] conversionCurrencies)
    {
      try
      {
        var request = await _httpClient.Request()
          

          .HttpClient.GetAsync(path)
          .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var responseOfT = await JsonSerializer.DeserializeAsync<List<CurrencyConversion>>(await response.Content.ReadAsStreamAsync());
        if (responseOfT == null)
          throw new ArgumentException(RESPONSERECEIVEDISNULL);

        return responseOfT;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}