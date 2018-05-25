using System;
using OxyEngine.Interfaces;

namespace OxyEngine.Loggers
{
  public class ConsoleLogger : ILogger
  {
    public void Log(string message)
    {
      LogWithColor(message, Console.ForegroundColor);
    }

    public void Warning(string message)
    {
      LogWithColor($"Warning: {message}", ConsoleColor.Yellow);
    }

    public void Error(string message)
    {
      LogWithColor($"ERROR: {message}", ConsoleColor.Red);
    }

    public void Save()
    {
      // Console logger saves nothing
    }

    private void LogWithColor(string message, ConsoleColor color)
    {    
      var now = DateTime.Now;
      var currentColor = Console.ForegroundColor;
      Console.ForegroundColor = color;
      Console.WriteLine($"[{now:G}] {message}");
      Console.ForegroundColor = currentColor;
    }
  }
}