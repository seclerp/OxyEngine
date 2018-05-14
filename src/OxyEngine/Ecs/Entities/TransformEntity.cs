using OxyEngine.Ecs.Components;

namespace OxyEngine.Ecs.Entities
{
  /// <summary>
  ///   Ready-to-use entity with attached TransformComponent
  /// </summary>
  public class TransformEntity : GameEntity
  {
    public TransformComponent Transform { get; }

    public TransformEntity()
    {
      Transform = AddComponent<TransformComponent>();
    }
  }
}