using System;
using OxyEngine;

namespace OxyPlayground
{
  public static class Program
  {
    [STAThread]
    static void Main(string[] args)
    {
      var projectLoader = new GameProjectLoader();
      var project = projectLoader.LoadFromArguments(args);
      
      using (var playground = new GameInstance(project))
      {
        playground.Run();
      }
    }
  }
}
