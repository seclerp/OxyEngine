using Microsoft.Xna.Framework;

namespace OxyEngine.Ecs.Components
{
  /// <summary>
  ///   Component for drawing lines
  /// </summary>
  public class LineComponent : FigureComponent
  {
    /// <summary>
    ///   Line thickness
    /// </summary>
    public float Thickness { get; set; }
    
    /// <summary>
    ///   Line start point
    /// </summary>
    public Vector2 From { get; set; }
    
    /// <summary>
    ///   Line end point
    /// </summary>
    public Vector2 To { get; set; }

    public LineComponent()
    {
      Thickness = 1;
      From = Vector2.Zero;
      To = Vector2.Zero;
    }
    
    public override void Draw()
    {
      var currColor = GraphicsManager.GetColor();
      var currThickness = GraphicsManager.GetLineWidth();
      
      GraphicsManager.SetColor(Color.R, Color.G, Color.B, Color.A);
      GraphicsManager.SetLineWidth(Thickness);
      GraphicsManager.Line(From.X, From.Y, To.X, To.Y);
      GraphicsManager.SetLineWidth(currThickness);
      GraphicsManager.SetColor(currColor.R, currColor.G, currColor.B, currColor.A);
    }
  }
}