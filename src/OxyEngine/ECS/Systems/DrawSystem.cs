using System.Linq;
using OxyEngine.ECS.Behaviours;
using OxyEngine.ECS.Components;
using OxyEngine.ECS.Entities;

namespace OxyEngine.ECS.Systems
{
  public class DrawSystem : BaseGameSystem, IDrawable
  {
    public DrawSystem(BaseGameEntity rootEntity) : base(rootEntity)
    {
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
  }
}