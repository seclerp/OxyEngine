using Microsoft.Xna.Framework;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Graphics;
using IDrawable = OxyEngine.Ecs.Behaviours.IDrawable;

namespace OxyEngine.Ecs.Components
{
  public abstract class FigureComponent : GameComponent, ILoadable, IDrawable
  {
    public Color Color;
    
    protected GraphicsManager GraphicsManager;

    public FigureComponent()
    {
      Color = Color.White;
    }
    
    public virtual void Load()
    {
      // This is for DrawSystem, to not fail when using GetComponent with NullReferenceExcepion
      RequireComponent<TransformComponent>();
      GraphicsManager = GetApi().Graphics;
    }

    public abstract void Draw();
  }
}