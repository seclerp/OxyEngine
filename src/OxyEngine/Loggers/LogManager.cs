using System.Collections.Generic;
using OxyEngine.Interfaces;

namespace OxyEngine.Loggers
{
  public static class LogManager
  {
    private static ICollection<ILogger> _loggers;

    static LogManager()
    {
      _loggers = new List<ILogger>();
    }
    
    public static void Log(string message)
    {
      foreach (var logger in _loggers)
      {
        logger.Log(message);
      }
    }

    public static void Warning(string message)
    {
      foreach (var logger in _loggers)
      {
        logger.Warning(message);
      }
    }
    
    public static void Error(string message)
    {
      foreach (var logger in _loggers)
      {
        logger.Error(message);
      }
    }

    public static void Save(string message)
    {
      foreach (var logger in _loggers)
      {
        logger.Error(message);
      }
    }
  }
}