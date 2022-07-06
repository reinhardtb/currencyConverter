namespace Knab.Library.ApiClient.Interfaces
{
  public interface IApiClientConfiguration
  {
    string BaseUri { get; set; }
    string ApiKey { get; set; }
  }
}