using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Graphics;
using IDrawable = OxyEngine.Ecs.Behaviours.IDrawable;

namespace OxyEngine.Ecs.Components
{
  /// <summary>
  ///   Component for drawing sprites
  /// </summary>
  public class SpriteComponent : GameComponent, ILoadable, IDrawable
  {
    /// <summary>
    ///   Rectangle on texture, used to determine position and size of a sprite
    /// </summary>
    public Rectangle SourceRectangle { get; set; }
    
    /// <summary>
    ///   Offset (origin) point
    /// </summary>
    public Vector2 Offset { get; set; }
    
    /// <summary>
    ///   Reference texture
    /// </summary>
    public Texture2D Texture { get; set; }

    private GraphicsManager _graphicsManager;
    
    public void Load()
    {
      // This is for DrawSystem, to not fail when using GetComponent with NullReferenceExcepion
      RequireComponent<TransformComponent>();

      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }

    public void Draw()
    {
      // Entity matrix is already applied
      _graphicsManager.Draw(Texture, SourceRectangle, new Rectangle(0, 0, SourceRectangle.Width, SourceRectangle.Height), Offset.X, Offset.Y);
    }
  }
}