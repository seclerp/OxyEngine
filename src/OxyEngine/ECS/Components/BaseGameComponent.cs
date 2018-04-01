using System;
using OxyEngine.ECS.Entities;
using OxyEngine.ECS.Systems;
using OxyEngine.Interfaces;

namespace OxyEngine.ECS.Components
{
  [UseGameSystem(GameSystems.LogicSystemName)]
  public abstract class BaseGameComponent : IUniqueObject
  {
    public Guid Id { get; }
    public BaseGameEntity Entity { get; private set; }
    public string SystemName { get; }
    
    protected BaseGameComponent(BaseGameEntity entity)
    {
      Id = Guid.NewGuid();
      SystemName = GetSystemName();
      
      SetEntity(entity);
    }

    protected BaseGameComponent() : this(null)
    {
    }

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
    
    private string GetSystemName()
    {
      var name = "";
      var attrs = GetType().GetCustomAttributes(false);
      foreach(UseGameSystemAttribute attr in attrs)
      {
        name = attr.SystemName;
      }

      return name;
    }
  }
}