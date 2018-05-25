using System;
using OxyEngine.Ecs.Entities;

namespace OxyEngine.Ecs.Components
{
  /// <summary>
  ///   Base class for every game component
  /// </summary>
  public abstract class GameComponent : UniqueObject
  {
    /// <summary>
    ///   Entity, which this component attached to
    /// </summary>
    public GameEntity Entity { get; private set; }

    protected GameComponent(GameEntity entity)
    {
      SetEntity(entity);
    }

    protected GameComponent() : this(null)
    {
    }

    public bool IsDetached()
    {
      return Entity == null;
    }

    internal void SetEntity(GameEntity entity)
    {
      Entity = entity;
    }

    protected T RequireComponent<T>() where T : GameComponent
    {
      var component = Entity.GetComponent<T>();
      if (component == null)
      {
        throw new Exception("There is no component of type '{typeof(T).Name}' attached, but it is required");
      }

      return component;
    }
  }
}