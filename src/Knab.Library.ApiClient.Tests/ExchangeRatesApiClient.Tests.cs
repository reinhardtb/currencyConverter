using Flurl.Http.Testing;
using Knab.Library.ApiClient.Models;
using System.Text.Json;

namespace Knab.Library.ApiClient.Tests
{
  public class Tests
  {
    private HttpTest _httpTest;

    [SetUp]
    public void Setup()
    {
      _httpTest = new HttpTest();
    }

    [Test]
    public async Task GivenValidConfigurationAndParameters_ShouldReturnCurrencyConversions()
    {
      //Setup
      _httpTest.RespondWith(Statics.ValidApiRequestResponse);
      var conversionClient = new ResourceAccess.ExchangeRatesApiClient(new ApiClientConfiguration { BaseUri = Statics.ValidUri, ApiKey = "some text" });
      var expectedResult = Statics.ValidApiRequestResponseResult;

      //Act 
      var currencyConversion = await conversionClient.GetCurrencyConversionAsync(Statics.ValidCurrencyConversionBaseCurrency, Statics.ValidCurrencyConversionTargetCurrencies);

      //Assert
      AreEqualByJson(expectedResult, currencyConversion);
    }

    [Test]
    public void GivenInvalidUriConfiguration_ShouldRaiseMeaningfulValidationException()
    {
      //Setup
      var conversionClient = new ResourceAccess.ExchangeRatesApiClient(new Models.ApiClientConfiguration { BaseUri = Statics.InvalidUri, ApiKey = "some text" });

      //Act and Assert
      var ex = Assert.ThrowsAsync<ArgumentException>(async () => await conversionClient.GetCurrencyConversionAsync(Statics.ValidCurrencyConversionBaseCurrency, Statics.ValidCurrencyConversionTargetCurrencies));
      Assert.That(ex.Message, Is.EqualTo("The API URI provided through configuration is invalid."));
    }

    [Test]
    public void GivenInvalidApiKeyConfiguration_ShouldRaiseMeaningfulUnauthorizedException()
    {
      //Setup
      _httpTest.RespondWith(string.Empty, 401);
      var conversionClient = new ResourceAccess.ExchangeRatesApiClient(new Models.ApiClientConfiguration { BaseUri = Statics.ValidUri, ApiKey = "some text" });

      //Act and Assert
      var ex = Assert.ThrowsAsync<ArgumentException>(async () => await conversionClient.GetCurrencyConversionAsync(Statics.ValidCurrencyConversionBaseCurrency, Statics.ValidCurrencyConversionTargetCurrencies));
      Assert.That(ex.Message, Is.EqualTo("The API key provided through configuration can not authorize the request."));
    }

    [Test]
    public void GivenInvalidCurrencyCodeInputParameter_ShouldRaiseMeaningfulNotFoundException()
    {
      //Setup
      _httpTest.RespondWith(string.Empty, 400);
      var conversionClient = new ResourceAccess.ExchangeRatesApiClient(new Models.ApiClientConfiguration { BaseUri = Statics.ValidUri, ApiKey = "some text" });

      //Act and Assert
      var ex = Assert.ThrowsAsync<ArgumentException>(async () => await conversionClient.GetCurrencyConversionAsync(Statics.ValidCurrencyConversionBaseCurrency, Statics.ValidCurrencyConversionTargetCurrencies));
      Assert.That(ex.Message, Is.EqualTo("The request failed. This could be due to the input currency code not being found."));
    }

    [Test]
    public void GivenNullResponse_ShouldRaiseMeaningfulException()
    {
      _httpTest.RespondWith(string.Empty);
      var conversionClient = new ResourceAccess.ExchangeRatesApiClient(new Models.ApiClientConfiguration { BaseUri = Statics.ValidUri, ApiKey = "some text" });

      //Act and Assert
      var ex = Assert.ThrowsAsync<ArgumentException>(async () => await conversionClient.GetCurrencyConversionAsync(Statics.ValidCurrencyConversionBaseCurrency, Statics.ValidCurrencyConversionTargetCurrencies));
      Assert.That(ex.Message, Is.EqualTo("The response received from the server could not be parsed to the given type."));
    }

    [Test]
    public void GivenUnsuccesfulResponse_ShouldRaiseMeaningfulException()
    {
      _httpTest.RespondWith(Statics.UnsuccessfulApiRequestResponse);
      var conversionClient = new ResourceAccess.ExchangeRatesApiClient(new Models.ApiClientConfiguration { BaseUri = Statics.ValidUri, ApiKey = "some text" });

      //Act and Assert
      var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await conversionClient.GetCurrencyConversionAsync(Statics.ValidCurrencyConversionBaseCurrency, Statics.ValidCurrencyConversionTargetCurrencies));
      Assert.That(ex.Message, Is.EqualTo("The request to the currency conversion provider was unsuccessful."));
    }

    [Test]
    public void GivenSuccesfulResponseWithNoConversionRates_ShouldRaiseMeaningfulException()
    {
      _httpTest.RespondWith(Statics.ApiRequestResponseWithNoConversionRates);
      var conversionClient = new ResourceAccess.ExchangeRatesApiClient(new Models.ApiClientConfiguration { BaseUri = Statics.ValidUri, ApiKey = "some text" });

      //Act and Assert
      var ex = Assert.ThrowsAsync<ArgumentException>(async () => await conversionClient.GetCurrencyConversionAsync(Statics.ValidCurrencyConversionBaseCurrency, Statics.ValidCurrencyConversionTargetCurrencies));
      Assert.That(ex.Message, Is.EqualTo("The request to the currency conversion provider did not return any conversion rates."));
    }

    public static void AreEqualByJson(object expected, object actual)
    {
      var expectedJson = JsonSerializer.Serialize(expected);
      var actualJson = JsonSerializer.Serialize(actual);
      Assert.That(expectedJson, Is.EqualTo(actualJson));
    }

    [TearDown]
    public void TearDown()
    {
      _httpTest.Dispose();
    }
  }
}