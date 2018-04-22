using System.Collections.Generic;
using System.Diagnostics;
using OxyEngine.Interfaces;

namespace OxyEngine.Loggers
{
  public static class LogManager
  {
    private static ICollection<ILogger> _loggers;

    public static bool LogCallerType { get; set; }
    
    static LogManager()
    {
      _loggers = new List<ILogger>();
    }

    public static void AddLogger(ILogger logger)
    {
      _loggers.Add(logger);
    }
    
    public static void RemoveLogger(ILogger logger)
    {
      _loggers.Remove(logger);
    }
    
    public static void Log(string message)
    {
      if (LogCallerType)
      {
        message = $"[{GetCallerInfo(2)}] {message}";
      }
      
      foreach (var logger in _loggers)
      {
        logger.Log(message);
      }
    }

    public static void Warning(string message)
    {
      if (LogCallerType)
      {
        message = $"[{GetCallerInfo(2)}] {message}";
      }
      
      foreach (var logger in _loggers)
      {
        logger.Warning(message);
      }
    }
    
    public static void Error(string message)
    {
      if (LogCallerType)
      {
        message = $"[{GetCallerInfo(2)}] {message}";
      }
      
      foreach (var logger in _loggers)
      {
        logger.Error(message);
      }
    }

    public static void Save()
    {
      foreach (var logger in _loggers)
      {
        logger.Save();
      }
    }

    private static string GetCallerInfo(int stacktraceDepth = 1)
    {
      var methodInfo = new StackTrace()
        .GetFrame(stacktraceDepth)
        .GetMethod();
      
      return 
        $"{methodInfo.ReflectedType?.Name}.{methodInfo.Name}";
    }
  }
}