using OxyEngine.ECS.Components;

namespace OxyEngine.ECS.Entities
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