using OxyEngine.UI.DataBinders;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Nodes;
using OxyEngine.UI.Renderers;

namespace OxyEngine.UI.Models
{
  public class HorizontalLayoutModel : LayoutWidgetModel
  {
    public VerticalAlignment VAlign { get; set; } = VerticalAlignment.FullHeight;
    
    public HorizontalLayoutModel(WidgetNode node) : base(node)
    {
    }
  }
}