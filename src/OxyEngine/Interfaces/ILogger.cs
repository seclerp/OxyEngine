namespace OxyEngine.Interfaces
{
  public interface ILogger
  {
    void Log(string message);
    void Warning(string message);
    void Error(string message);
    void Save();
  }
}