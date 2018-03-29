using System;
using OxyEngine.ECS.Entities;

namespace OxyEngine.ECS.Components
{
  public abstract class BaseGameComponent : IUniqueObject
  {
    public Guid Id { get; }
    public BaseGameEntity Entity { get; private set; }

    protected BaseGameComponent(BaseGameEntity entity)
    {
      Id = Guid.NewGuid();

      SetEntity(entity);
    }

    internal void SetEntity(BaseGameEntity entity)
    {
      Entity = entity;
    }

    public bool IsDetached()
    {
      return Entity == null;
    }
  }
}