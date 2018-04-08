using System;
using Microsoft.Xna.Framework;
using OxyEngine.Ecs;
using OxyEngine.Ecs.Components;
using OxyEngine.Ecs.Entities;
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
        var entity = new GameEntity();
        var sprite = entity.AddComponent<SpriteComponent>();

        sprite.Texture = ecsInstance.GetApi().Resources.LoadTexture("example");
        sprite.SourceRectangle = new Rectangle(0, 0, sprite.Texture.Width, sprite.Texture.Height);
        sprite.Offset = new Vector2(0, 0);

        ecsInstance.SetRootEntity(entity);

        ecsInstance.Run();
      }
    }
  }
}