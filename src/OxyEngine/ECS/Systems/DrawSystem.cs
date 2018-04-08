using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Components;
using OxyEngine.Ecs.Entities;

namespace OxyEngine.Ecs.Systems
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
      
      if (entity is IDrawable entityDrawable)
        entityDrawable.Draw();

      foreach (var component in entity.Components)
      {
        if (component is IDrawable componentDrawable)
          componentDrawable.Draw();
      }
      
      foreach (var child in entity.Children)
      {
        DrawRecursive(child);
      }
      
      transform?.DetachTransformation();
    }
  }
}