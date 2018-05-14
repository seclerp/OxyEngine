using OxyEngine.UI.Models;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.Renderers
{
  public class ImageRenderer : WidgetRenderer
  {
    private readonly ImageModel _model;
    
    public ImageRenderer(WidgetNode node) : base(node)
    {
      _model = node.Model as ImageModel;
    }
    
    public override void Render()
    {
      var beforeColor = GraphicsApi.GetColor();
      GraphicsApi.SetColor(_model.BackgroundColor.R, _model.BackgroundColor.G, _model.BackgroundColor.B, _model.BackgroundColor.A);
      GraphicsApi.Rectangle("fill", (int) _model.X, (int) _model.Y, (int) _model.Width, (int) _model.Height);
      
      // TODO
    }
  }
}