namespace OxyEngine.Ecs.Components.Graphics
{
  /// <summary>
  ///   Component for drawing circles
  /// </summary>
  public class CircleComponent : FigureComponent
  {
    /// <summary>
    ///   Radius of a circle
    /// </summary>
    public float Radius { get; set; }

    public CircleComponent()
    {
      Radius = 100;
    }
    
    public override void Draw()
    {
      var currColor = GraphicsManager.GetColor();
      
      GraphicsManager.SetColor(Color.R, Color.G, Color.B, Color.A);
      GraphicsManager.Circle(0, 0, Radius);
      GraphicsManager.SetColor(currColor.R, currColor.G, currColor.B, currColor.A);
    }
  }
}