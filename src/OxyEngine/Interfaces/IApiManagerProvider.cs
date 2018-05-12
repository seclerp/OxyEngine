namespace OxyEngine.Interfaces
{
  /// <summary>
  ///   Interface for every class that can provide ApiManager object
  /// </summary>
  public interface IApiManagerProvider
  {
    ApiManager GetApiManager();
  }
}