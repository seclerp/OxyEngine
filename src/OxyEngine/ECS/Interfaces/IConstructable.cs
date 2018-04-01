using System.Collections.Generic;
using OxyEngine.ECS.Components;

namespace OxyEngine.ECS.Interfaces
{
  /// <summary>
  ///   Interface for entities that may have components
  /// </summary>
  public interface IConstructable
  {
    void AddComponent(BaseGameComponent component);
    T AddComponent<T>() where T : BaseGameComponent;
    T GetComponent<T>() where T : BaseGameComponent;
    IEnumerable<T> GetComponents<T>() where T : BaseGameComponent;
    bool RemoveComponent(BaseGameComponent component);
    bool RemoveComponent<T>() where T : BaseGameComponent;
    bool RemoveComponents<T>() where T : BaseGameComponent;
  }
}