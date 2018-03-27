using System.Collections.Generic;

namespace OxyEngine.ECS.Iterfaces
{
  /// <summary>
  ///   Interface for Entities that may have children
  /// </summary>
  public interface IParent
  {
    void AddChild(GameEntity child);
    T AddChild<T>() where T : GameEntity;
    T GetChild<T>() where T : GameEntity;
    IEnumerable<T> GetChildren<T>() where T : GameEntity;
    IEnumerable<GameEntity> GetChildren();
    bool RemoveChild(GameEntity entity);
    bool RemoveChild<T>() where T : GameEntity;
    bool RemoveChildren<T>() where T : GameEntity;
  }
}