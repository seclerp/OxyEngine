using System.Collections.Generic;
using OxyEngine.ECS.Components;
using OxyEngine.Loggers;

namespace OxyEngine.ECS.Systems
{
  public class GameSystemManager
  {
    private Dictionary<string, BaseGameSystem> _systems;

    public GameSystemManager()
    {
      _systems = new Dictionary<string, BaseGameSystem>();
      
      _systems[GameSystems.LogicSystemName] = new LogicSystem(GameSystems.LogicSystemName);
    }
    
    public 
  }
}