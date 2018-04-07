using OxyEngine.Ecs.Systems;

namespace OxyEngine.Ecs.Interfaces
{
  public interface IUpdateCallable
  {
    [GameSystemAction("draw")]
    void OnUpdate(float deltaTime);
  }
}