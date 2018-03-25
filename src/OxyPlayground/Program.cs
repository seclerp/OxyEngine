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
      
      using (var playground = new GameInstance(project))
      {
        playground.SetScripting(new PythonScripting());
        var api = playground.GetApi();
        
        api.Events.Global.StartListening("before-load", 
          (sender, eventArgs) => api.Scripting.ExecuteScript(project.EntryScriptName)
        );
        
        playground.Run();
      }
    }
  }
}
