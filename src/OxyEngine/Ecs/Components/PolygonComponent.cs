using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace OxyEngine.Ecs.Components
{
  /// <summary>
  ///   Component for drawing polygons
  /// </summary>
  public class PolygonComponent : FigureComponent
  {
    /// <summary>
    ///   Corners coordinates
    /// </summary>
    public List<Vector2> Points;

    public PolygonComponent()
    {
      Points = new List<Vector2>();
    }
    
    public override void Draw()
    {
      var currColor = GraphicsManager.GetColor();
      
      GraphicsManager.SetColor(Color.R, Color.G, Color.B, Color.A);
      GraphicsManager.Polygon(Points);
      GraphicsManager.SetColor(currColor.R, currColor.G, currColor.B, currColor.A);
    }
  }
}