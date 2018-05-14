using Microsoft.Xna.Framework;

namespace OxyEngine.Ecs.Components
{
  /// <summary>
  ///   Component for drawing points
  /// </summary>
  public class PointComponent : FigureComponent
  {
    /// <summary>
    ///   Point coordinates
    /// </summary>
    public Vector2 Point { get; set; }

    public PointComponent()
    {
      Point = Vector2.Zero;
    }
    
    public override void Draw()
    {
      var currColor = GraphicsManager.GetColor();
      var currThickness = GraphicsManager.GetLineWidth();
      
      GraphicsManager.SetColor(Color.R, Color.G, Color.B, Color.A);
      GraphicsManager.Point(Point.X, Point.Y);
      GraphicsManager.SetColor(currColor.R, currColor.G, currColor.B, currColor.A);
    }
  }
}