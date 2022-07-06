using Knab.Library.ApiClient.Interfaces;

namespace Knab.Library.ApiClient.Models
{
  public class ApiClientConfiguration : IApiClientConfiguration
  {
    public string BaseUri { get; set; }
    public string ApiKey { get; set; }
  }
}