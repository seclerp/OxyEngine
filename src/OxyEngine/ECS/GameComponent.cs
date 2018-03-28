using System;

namespace OxyEngine.ECS
{
  public abstract class GameComponent : IUniqueObject
  {
    public Guid Id { get; }
    public GameEntity Entity { get; private set; }

    protected GameComponent(GameEntity entity)
    {
      Id = Guid.NewGuid();
      
      if (Entity == null)
        throw new NullReferenceException(nameof(entity));
      
      Entity = entity;
    }

    internal void SetEntity(GameEntity entity)
    {
      Entity = entity;
    }
  }
}