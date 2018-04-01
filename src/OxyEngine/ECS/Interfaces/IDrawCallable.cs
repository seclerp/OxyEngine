using OxyEngine.ECS.Systems;

namespace OxyEngine.ECS.Interfaces
{
  public interface IDrawCallable
  {
    [GameSystemAction("draw")]
    void OnDraw();
  }
}