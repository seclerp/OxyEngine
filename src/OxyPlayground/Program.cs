using System;
using OxyEngine;
using OxyEngine.Loggers;

namespace OxyPlayground
{
  public static class Program
  {
    [STAThread]
    static void Main(string[] args)
    {
      LogManager.AddLogger(new ConsoleLogger());

      var projectLoader = new GameProjectLoader();
      var project = projectLoader.LoadFromArguments(args);
      
      using (var playground = new PlaygroundInstance(project))
      {
        playground.Run();
      }
    }
  }
}
