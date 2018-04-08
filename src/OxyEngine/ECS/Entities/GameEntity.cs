using OxyEngine.Ecs.Components;

namespace OxyEngine.Ecs.Entities
{
  public class GameEntity : BaseGameEntity
  {
    public TransformComponent Transform { get; }

    public GameEntity()
    {
      Transform = AddComponent<TransformComponent>();
    }
  }
}