using Microsoft.Xna.Framework;
using OxyEngine.UI.DataBinders;
using OxyEngine.UI.Renderers;

namespace OxyEngine.UI.Widgets
{
  public class GraphicsWidget : Widget
  {
    public Color BackgroundColor { get; set; } = Color.Transparent;
    public Color ForegroundColor { get; set; } = Color.White;
    
    public GraphicsWidget(WidgetRenderer renderer, WidgetDataBinder dataBinder) : base(renderer, dataBinder)
    {
    }
  }
}