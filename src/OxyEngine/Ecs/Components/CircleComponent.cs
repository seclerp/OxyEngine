namespace OxyEngine.Ecs.Components
{
  public class CircleComponent : FigureComponent
  {
    public float Radius;

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