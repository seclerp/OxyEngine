using OxyEngine.ECS.Components;
using OxyEngine.ECS.Entities;

namespace OxyEngine.ECS
{
  public class GameEntity : BaseGameEntity
  {
    public TransformComponent Transform { get; private set; }
  }
}