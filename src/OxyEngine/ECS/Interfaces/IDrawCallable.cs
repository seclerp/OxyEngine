using OxyEngine.Ecs.Systems;

namespace OxyEngine.Ecs.Interfaces
{
  public interface IDrawCallable
  {
    [GameSystemAction("draw")]
    void OnDraw();
  }
}