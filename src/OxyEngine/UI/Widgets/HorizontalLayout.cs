using OxyEngine.UI.DataBinders;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Renderers;

namespace OxyEngine.UI.Widgets
{
  public class HorizontalLayout : LayoutWidget
  {
    public VerticalAlignment VAlign { get; set; } = VerticalAlignment.FullHeight;
    
    public HorizontalLayout(WidgetRenderer renderer, WidgetDataBinder dataBinder) : base(renderer, dataBinder)
    {
    }
  }
}