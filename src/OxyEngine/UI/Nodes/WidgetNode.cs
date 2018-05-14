using OxyEngine.UI.DataBinders;
using OxyEngine.UI.Models;
using OxyEngine.UI.Renderers;

namespace OxyEngine.UI.Nodes
{
  public class WidgetNode
  {
    public WidgetModel Model { get; }
    public WidgetRenderer Renderer { get; }
    public WidgetDataBinder DataBinder { get; }

    public WidgetNode(WidgetModel model, WidgetRenderer renderer, WidgetDataBinder dataBinder)
    {
      Model = model;
      Renderer = renderer;
      DataBinder = dataBinder;
    }
  }
}