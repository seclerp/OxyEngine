using System.Collections.Generic;
using OxyEngine.ECS.Components;

namespace OxyEngine.ECS.Systems
{
  public class GameSystemManager
  {
    private Dictionary<string, BaseGameSystem> _systems;

    public GameSystemManager()
    {
      _systems = new Dictionary<string, BaseGameSystem>();
      
      // TODO
      //_systems[GameSystems.LogicSystemName] = new LogicSystem();
    }

    public void AttachComponent(BaseGameComponent component)
    {
      if (!_systems.ContainsKey(component.SystemName))
      {
        throw new KeyNotFoundException(component.SystemName);
      }
      
      _systems[component.SystemName].AddComponent(component);
    }
    
    public void DetachComponent(BaseGameComponent component)
    {
      if (!_systems.ContainsKey(component.SystemName))
      {
        throw new KeyNotFoundException(component.SystemName);
      }
      
      _systems[component.SystemName].RemoveComponent(component);
    }
  }
}