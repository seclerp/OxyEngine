using Microsoft.Xna.Framework;
using OxyEngine.Ecs.Enums;

namespace OxyEngine.Ecs.Components
{
  public class RectangleComponent : FigureComponent
  {
    public Rectangle Rectangle;
    public FigureFillStyle FillStyle;

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