using System;

namespace Oxy.Playground
{
  public static class Program
  {
    [STAThread]
    static void Main(string[] args)
    {
      var projectLoader = new PlaygroundProjectLoader();
      var project = projectLoader.LoadFromArguments(args);
      
      using (var playground = new PlaygroundInstance(project))
      {
        playground.Run();
      }
    }
  }
}
