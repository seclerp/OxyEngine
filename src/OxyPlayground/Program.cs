using System;
using OxyEngine.Loggers;
using OxyEngine.Projects;

namespace OxyPlayground
{
  public static class Program
  {
    [STAThread]
    static void Main(string[] args)
    {
      LogManager.LogCallerType = true;
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
