using OxyEngine.Ecs.Components;

namespace OxyEngine.Ecs.Entities
{
  public class GameEntity : BaseGameEntity
  {
    public TransformComponent Transform { get; }

    public GameEntity(GameInstance game) : base(game)
    {
      Transform = AddComponent<TransformComponent>();
    }
  }
}