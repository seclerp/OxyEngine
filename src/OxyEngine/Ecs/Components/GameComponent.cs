using System;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Entities;
using OxyEngine.Ecs.Systems;
using OxyEngine.Events;
using OxyEngine.Interfaces;

namespace OxyEngine.Ecs.Components
{
  public abstract class GameComponent : UniqueObject, IApiUser
  {
    public GameEntity Entity { get; private set; }
    public string SystemName { get; }

    private EventSystem _eventSystem;
    
    protected GameComponent(GameEntity entity)
    {
      _eventSystem = new EventSystem();
      _eventSystem.AddListenersFromAttributes(this);
      
      SetEntity(entity);
    }

    protected GameComponent() : this(null)
    {
    }

    [ListenEvent(EventNames.Initialization.OnInit)]
    public virtual void OnInit()
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

    public IOxyApi GetApi()
    {
      return Container.Instance.ResolveByName<IOxyApi>(InstanceName.Api);
    }
  }
}