using Microsoft.Xna.Framework;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Graphics;
using IDrawable = OxyEngine.Ecs.Behaviours.IDrawable;

namespace OxyEngine.Ecs.Components.UI
{
  public class UIComponent : GameComponent, IInitializable, IDrawable
  {
    public Rectangle Rectangle { get; set; }
    protected GraphicsManager GraphicsManager;
    
    public void Init()
    {
      GraphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }
    
    public virtual void Draw()
    {
    }
  }
}