using OxyEngine.Ecs.Entities;

namespace OxyEngine.Ecs.Systems
{
  public class GameSystem
  {
    protected GameEntity RootEntity;
    
    public GameSystem(GameEntity rootEntity)
    {
      RootEntity = rootEntity;
    }
  }
}