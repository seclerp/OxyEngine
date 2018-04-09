using System;
using Microsoft.Xna.Framework;
using OxyEngine.Ecs;
using OxyEngine.Ecs.Components;
using OxyEngine.Ecs.Entities;
using OxyEngine.Events;
using OxyEngine.Loggers;
using OxyEngine.Projects;

namespace OxyEngine.Game
{
  public static class Program
  {
    [STAThread]
    private static void Main(string[] args)
    {
      LogManager.LogCallerType = true;
      LogManager.AddLogger(new ConsoleLogger());

      var projectLoader = new GameProjectLoader();
      var project = projectLoader.LoadFromArguments(args);

      using (var ecsInstance = new EcsInstance(project))
      {
        ecsInstance.GetApi().Events.Global.LogListenerRegistration = true;
        ecsInstance.GetApi().Events.Global.StartListening(
          EventNames.Initialization.OnInit, 
          (sender, eventArgs) =>
          {
            var gameScene = new TransformScene();
            ecsInstance.SetRootEntity(gameScene);
          }
        );
        ecsInstance.InitializeEventListeners();

        ecsInstance.Run();
      }
    }
  }
}