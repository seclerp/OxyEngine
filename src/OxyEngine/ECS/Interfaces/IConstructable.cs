using System.Collections.Generic;

namespace OxyEngine.ECS.Iterfaces
{
  /// <summary>
  ///   Interface for entities that may have components
  /// </summary>
  public interface IConstructable
  {
    void AddComponent(GameComponent component);
    void AddComponent<T>() where T : GameComponent;
    T GetComponent<T>() where T : GameComponent;
    IEnumerable<T> GetComponents<T>() where T : GameComponent;
    bool RemoveComponent(GameComponent component);
    bool RemoveComponent<T>() where T : GameComponent;
    bool RemoveComponents<T>() where T : GameComponent;
  }
}