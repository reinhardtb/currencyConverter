while (true)
{
  Console.WriteLine("Please enter a base cryptocurrency code for conversion, or [x] to exit: ");
  var input = Console.ReadLine();

  if (string.IsNullOrEmpty(input))
    Console.WriteLine("No input was received.");
  else if (input.Equals("x", StringComparison.OrdinalIgnoreCase))
    break;
  else if (input.Trim().Length != 3)
    Console.WriteLine("Please enter a valid ISO currency code.");
  else
  {
    var apiConfiguration = new Knab.Library.ApiClient.Models.ApiClientConfiguration { BaseUri = "https://api.apilayer.com/exchangerates_data/", ApiKey = "mBkNrbmbFMjIjIGQTN7F0gOZawBrm6tK" };
    var apiClient = new Knab.Library.ApiClient.ResourceAccess.ExchangeRatesApiClient(apiConfiguration);

    try
    {
      var conversions = await apiClient.GetCurrencyConversionAsync(input, new[] { "USD", "EUR", "BRL", "GBP", "AUD" });

      Console.WriteLine($"Currency conversion rates for [{input.ToUpper()}] at this point in time are:");
      foreach (var conversion in conversions)
        Console.WriteLine($"{conversion.CurrencyCode}: {conversion.ConversionRate}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error was experienced retrieving currency conversion rates: {ex.Message}");
    }
  }

  Console.ReadKey();
  Console.Clear();
}