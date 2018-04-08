using System;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Entities;
using OxyEngine.Ecs.Systems;
using OxyEngine.Events;
using OxyEngine.Interfaces;

namespace OxyEngine.Ecs.Components
{
  public abstract class BaseGameComponent : IUniqueObject, IApiUser
  {
    public Guid Id { get; }
    public BaseGameEntity Entity { get; private set; }
    public string SystemName { get; }

    private EventSystem _eventSystem;
    
    protected BaseGameComponent(BaseGameEntity entity)
    {
      Id = Guid.NewGuid();
      _eventSystem = new EventSystem();
      _eventSystem.AddListenersFromAttributes(this);
      
      SetEntity(entity);
    }

    protected BaseGameComponent() : this(null)
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

    internal void SetEntity(BaseGameEntity entity)
    {
      Entity = entity;
    }

    protected T RequireComponent<T>() where T : BaseGameComponent
    {
      var component = Entity.GetComponent<T>();
      if (component == null)
      {
        throw new Exception("There is no component of type '{typeof(T).Name}' attached, but it is required");
      }

      return component;
    }

    public OxyApi GetApi()
    {
      return Container.Instance.ResolveByName<OxyApi>(InstanceName.Api);
    }
  }
}