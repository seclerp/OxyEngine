namespace OxyEngine.ECS
{
  public abstract class GameComponent
  {
    public GameEntity Entity { get; }

    protected GameComponent(GameEntity entity)
    {
      Entity = entity;
    }
  }
}