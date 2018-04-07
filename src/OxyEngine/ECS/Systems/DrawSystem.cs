using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Components;
using OxyEngine.Ecs.Entities;
using OxyEngine.Interfaces;

namespace OxyEngine.Ecs.Systems
{
  public class DrawSystem : BaseGameSystem, IDrawable, IApiUser
  {
    private readonly GameInstance _instance;

    public DrawSystem(GameInstance instance, BaseGameEntity rootEntity) : base(rootEntity)
    {
      _instance = instance;
    }
    
    public void Draw()
    {
      DrawRecursive(RootEntity);
    }

    private void DrawRecursive(BaseGameEntity entity)
    {
      var transform =  entity.GetComponent<TransformComponent>();
      transform?.AttachTransformation();
      
      if (entity is IDrawable rootEntityDrawable)
        rootEntityDrawable.Draw();

      foreach (var drawableChildren in RootEntity.Children)
      {
        DrawRecursive(drawableChildren);
      }
      
      transform?.DetachTransformation();
    }

    public OxyApi GetApi()
    {
      return _instance.GetApi();
    }
  }
}