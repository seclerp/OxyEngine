using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Graphics;

namespace OxyEngine.Ecs.Components
{
  public class SpriteComponent : GameComponent, ILoadable, Behaviours.IDrawable
  {
    public Rectangle SourceRectangle { get; set; }
    public Vector2 Offset { get; set; }
    public Texture2D Texture { get; set; }

    private GraphicsManager _graphicsManager;
    
    public void Load()
    {
      // This is for DrawSystem, to not fail when using GetComponent with NullReferenceExcepion
      RequireComponent<TransformComponent>();

      _graphicsManager = GetApi().Graphics;
    }

    public void Draw()
    {
      // Entity matrix is already applied
      _graphicsManager.Draw(Texture, SourceRectangle, new Rectangle(0, 0, SourceRectangle.Width, SourceRectangle.Height), Offset.X, Offset.Y);
    }
  }
}