using Microsoft.Xna.Framework;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.Models
{
  public class GraphicsWidgetModel : WidgetModel
  {
    public Color BackgroundColor { get; set; } = Color.Transparent;
    public Color ForegroundColor { get; set; } = Color.White;

    public GraphicsWidgetModel(WidgetNode node) : base(node)
    {
    }
  }
}