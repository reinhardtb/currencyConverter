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
      var httpResponse = @"
{
	""success"": true,
	""timestamp"": 1657127376,
	""base"": ""BTC"",
	""date"": ""2022-07-06"",
	""rates"": {
		""USD"": 20189.791,
		""EUR"": 19843.515895,
		""BRL"": 110134.459306,
		""GBP"": 16957.432105,
		""AUD"": 29831.254083
	}
}";
      _httpTest.RespondWith(httpResponse);
      var conversionClient = new ResourceAccess.ExchangeRatesApiClient(new Models.ApiClientConfiguration { BaseUri = "https://a.valid.url/", ApiKey = "some text" });
      var expectedResult = new List<CurrencyConversion>
      {
        new CurrencyConversion { CurrencyCode = "USD", ConversionRate = 20189.791 },
        new CurrencyConversion { CurrencyCode = "EUR", ConversionRate = 19843.515895 },
        new CurrencyConversion { CurrencyCode = "BRL", ConversionRate = 110134.459306 },
        new CurrencyConversion { CurrencyCode = "GBP", ConversionRate = 16957.432105 },
        new CurrencyConversion { CurrencyCode = "AUD", ConversionRate = 29831.254083 }
      };

      //Act 
      var currencyConversion = await conversionClient.GetCurrencyConversionAsync("BTC", new[] { "USD", "EUR", "BRL", "GBP", "AUD" });

      //Assert
      AreEqualByJson(expectedResult, currencyConversion);
    }

    // GivenInvalidUriConfiguration_ShouldRaiseMeaningfulValidationException
    // GivenInvalidApiKeyConfiguration_ShouldRaiseMeaningfulUnauthorizedException (401 unauthorized)
    // GivenInvalidCurrencyCodeParameter_ShouldRaiseMeaningfulNotFoundException (400 bad request)

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