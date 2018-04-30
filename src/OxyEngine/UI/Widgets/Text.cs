using Microsoft.Xna.Framework.Graphics;
using OxyEngine.UI.DataBinders;
using OxyEngine.UI.Renderers;

namespace OxyEngine.UI.Widgets
{
  public class Text : GraphicsWidget
  {
    public SpriteFont Font { get; set; }
    public string Value { get; set; }
    
    public Text(WidgetRenderer renderer, WidgetDataBinder dataBinder) : base(renderer, dataBinder)
    {
    }
  }
}