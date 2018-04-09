using OxyEngine.Ecs.Components;

namespace OxyEngine.Ecs.Entities
{
  public class TransformEntity : GameEntity
  {
    public TransformComponent Transform { get; }

    public TransformEntity()
    {
      Transform = AddComponent<TransformComponent>();
    }
  }
}