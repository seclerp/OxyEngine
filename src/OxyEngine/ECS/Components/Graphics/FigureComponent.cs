using Microsoft.Xna.Framework;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Graphics;
using IDrawable = OxyEngine.Ecs.Behaviours.IDrawable;

namespace OxyEngine.Ecs.Components.Graphics
{
  /// <summary>
  ///   Component base for drawing figures 
  /// </summary>
  public abstract class FigureComponent : GameComponent, ILoadable, IDrawable
  {
    /// <summary>
    ///   Main drawing color
    /// </summary>
    public Color Color { get; set; }
    
    protected GraphicsManager GraphicsManager;

    public FigureComponent()
    {
      Color = Color.White;
    }
    
    public virtual void Load()
    {
      // This is for DrawSystem, to not fail when using GetComponent with NullReferenceExcepion
      RequireComponent<TransformComponent>();
      GraphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }

    public abstract void Draw();
  }
}