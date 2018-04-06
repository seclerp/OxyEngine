using System;
using OxyEngine.ECS.Entities;
using OxyEngine.Events;
using OxyEngine.Interfaces;

namespace OxyEngine.ECS.Components
{
  public abstract class BaseGameComponent : IUniqueObject
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
  }
}