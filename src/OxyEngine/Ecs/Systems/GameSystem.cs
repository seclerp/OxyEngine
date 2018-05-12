using OxyEngine.Ecs.Entities;

namespace OxyEngine.Ecs.Systems
{
  /// <summary>
  ///   Base class for every game system
  /// </summary>
  public class GameSystem
  {
    protected GameEntity RootEntity;
    
    public GameSystem(GameEntity rootEntity)
    {
      RootEntity = rootEntity;
    }
  }
}