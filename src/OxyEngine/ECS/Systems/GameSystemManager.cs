using System.Collections.Generic;
using OxyEngine.ECS.Behaviours;
using OxyEngine.ECS.Components;
using OxyEngine.ECS.Entities;
using OxyEngine.Events;
using OxyEngine.Events.Args;
using OxyEngine.Loggers;

namespace OxyEngine.ECS.Systems
{
  public class GameSystemManager : IUpdateable, IRenderable
  {
    public LogicSystem LogicSystem { get; private set; }

    private GlobalEventsManager _events;
    
    public GameSystemManager(GameInstance gameInstance, BaseGameEntity rootEntity)
    {
      LogicSystem = new LogicSystem(rootEntity);

      _events = gameInstance.GetApi().Events;
      _events.Global.StartListening(EventNames.Gameloop.OnUpdate, 
        (sender, args) => Update((float)((EngineUpdateEventArgs)args).DeltaTime)
      );
    }

    public void Update(float dt)
    {
      LogicSystem.Update(dt);
    }
  }
}