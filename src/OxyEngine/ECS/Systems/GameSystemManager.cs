using System.Collections.Generic;
using OxyEngine.ECS.Behaviours;
using OxyEngine.ECS.Components;
using OxyEngine.ECS.Entities;
using OxyEngine.Events;
using OxyEngine.Events.Args;
using OxyEngine.Loggers;

namespace OxyEngine.ECS.Systems
{
  public class GameSystemManager : IUpdateable, IDrawable
  {
    public LogicSystem LogicSystem { get; }
    public DrawSystem DrawSystem { get; }

    private GlobalEventsManager _events;
    
    public GameSystemManager(GameInstance gameInstance, BaseGameEntity rootEntity)
    {
      LogicSystem = new LogicSystem(rootEntity);
      DrawSystem = new DrawSystem(rootEntity);

      _events = gameInstance.GetApi().Events;
      _events.Global.StartListening(EventNames.Initialization.OnLoad, 
        (sender, args) => Load()
      );
      _events.Global.StartListening(EventNames.Gameloop.OnUpdate, 
        (sender, args) => Update((float)((EngineUpdateEventArgs)args).DeltaTime)
      );
      _events.Global.StartListening(EventNames.Graphics.OnDraw, 
        (sender, args) => Draw()
      );
    }

    public void Load()
    {
      LogicSystem.Load();
    }
    
    public void Update(float dt)
    {
      LogicSystem.Update(dt);
    }

    public void Draw()
    {
      DrawSystem.Draw();
    }
  }
}