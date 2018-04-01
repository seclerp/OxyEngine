using OxyEngine.ECS.Systems;

namespace OxyEngine.ECS.Interfaces
{
  public interface IUpdateCallable
  {
    [GameSystemAction("draw")]
    void OnUpdate(float deltaTime);
  }
}