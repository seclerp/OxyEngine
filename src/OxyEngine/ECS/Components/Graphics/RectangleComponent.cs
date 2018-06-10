using Microsoft.Xna.Framework;
using OxyEngine.Ecs.Enums;

namespace OxyEngine.Ecs.Components.Graphics
{
  /// <summary>
  ///   Component for drawing rectangles
  /// </summary>
  public class RectangleComponent : FigureComponent
  {
    /// <summary>
    ///   Coordinate and size of rectangle to draw
    /// </summary>
    public Rectangle Rectangle { get; set; }
    
    /// <summary>
    ///   Fill style
    /// </summary>
    public FigureFillStyle FillStyle { get; set; }

    public RectangleComponent()
    {
      Rectangle = Rectangle.Empty;
      FillStyle = FigureFillStyle.Line;
    }
    
    public override void Draw()
    {
      var currColor = GraphicsManager.GetColor();
      
      GraphicsManager.SetColor(Color.R, Color.G, Color.B, Color.A);
      GraphicsManager.Rectangle(FillStyle == FigureFillStyle.Fill ? "fill" : "line", Rectangle.X, Rectangle.Y, Rectangle.Width, Rectangle.Height);
      GraphicsManager.SetColor(currColor.R, currColor.G, currColor.B, currColor.A);
    }
  }
}