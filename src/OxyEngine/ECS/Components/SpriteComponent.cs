using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.ECS.Behaviours;
using OxyEngine.Graphics;
using IDrawable = OxyEngine.ECS.Behaviours.IDrawable;

namespace OxyEngine.ECS.Components
{
  public class SpriteComponent : BaseGameComponent, ILoadable, IDrawable
  {
    public Rectangle SourceRectangle { get; set; }
    public Vector2 Offset { get; set; }
    public Texture2D Texture { get; set; }

    private GraphicsManager _graphicsManager;
    private TransformComponent _transform;
    
    public void Load()
    {
      _graphicsManager = Entity.Game.GetApi().Graphics;
      _transform = Entity.GetComponent<TransformComponent>();
    }

    public void Draw()
    {
      // Entity matrix is already applied
      _graphicsManager.Draw(Texture, SourceRectangle, new Rectangle(0, 0, SourceRectangle.Width, SourceRectangle.Height), Offset.X, Offset.Y);
    }
  }
}