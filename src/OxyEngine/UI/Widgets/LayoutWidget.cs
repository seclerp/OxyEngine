using OxyEngine.UI.DataBinders;
using OxyEngine.UI.Renderers;

namespace OxyEngine.UI.Widgets
{
  public abstract class LayoutWidget : ContainerWidget
  {
    protected LayoutWidget(WidgetRenderer renderer, WidgetDataBinder dataBinder) : base(renderer, dataBinder)
    {
    }
  }
}