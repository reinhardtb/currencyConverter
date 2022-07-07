using Knab.Library.ApiClient.Models;

namespace Knab.Library.ApiClient.Tests
{
	internal static class Statics
	{
		internal static string ValidUri = "https://a.valid.url/";
		internal static string InvalidUri = "https://an invalid url/";
		internal static string ValidCurrencyConversionBaseCurrency = "BTC";
		internal static string[] ValidCurrencyConversionTargetCurrencies = new[] { "USD", "EUR", "BRL", "GBP", "AUD" };

		internal static string UnsuccessfulApiRequestResponse = @"
{
	""success"": false,
	""timestamp"": 1657127376,
}";

		internal static string ApiRequestResponseWithNoConversionRates = @"
{
	""success"": true,
	""timestamp"": 1657127376,
	""base"": ""BTC"",
	""date"": ""2022-07-06""
}";

		internal static string ValidApiRequestResponse = @"
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

		internal static List<CurrencyConversion> ValidApiRequestResponseResult = new List<CurrencyConversion>
			{
				new CurrencyConversion { CurrencyCode = "USD", ConversionRate = 20189.791 },
				new CurrencyConversion { CurrencyCode = "EUR", ConversionRate = 19843.515895 },
				new CurrencyConversion { CurrencyCode = "BRL", ConversionRate = 110134.459306 },
				new CurrencyConversion { CurrencyCode = "GBP", ConversionRate = 16957.432105 },
				new CurrencyConversion { CurrencyCode = "AUD", ConversionRate = 29831.254083 }
			};
	}
}