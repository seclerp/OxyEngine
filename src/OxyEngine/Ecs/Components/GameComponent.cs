using System;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Entities;
using OxyEngine.Events;
using OxyEngine.Events.Args;
using OxyEngine.Interfaces;

namespace OxyEngine.Ecs.Components
{
  /// <summary>
  ///   Base class for every game component
  /// </summary>
  public abstract class GameComponent : UniqueObject, IApiManagerProvider
  {
    /// <summary>
    ///   Entity, which this component attached to
    /// </summary>
    public GameEntity Entity { get; private set; }

    private EventSystem _eventSystem;
    
    protected GameComponent(GameEntity entity)
    {
      _eventSystem = new EventSystem();
      _eventSystem.AddListenersUsingAttributes(this);
      
      SetEntity(entity);
    }

    protected GameComponent() : this(null)
    {
    }

    [ListenEvent(EventNames.Initialization.OnInit)]
    public virtual void OnInit(object sender, EngineEventArgs args)
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

    public ApiManager GetApiManager()
    {
      return Container.Instance.ResolveByName<ApiManager>(InstanceName.ApiManager);
    }
  }
}