using Microsoft.Xna.Framework.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.Models
{
  public class TextModel : GraphicsWidgetModel
  {
    public SpriteFont Font { get; set; }
    public string Value { get; set; }
    
    public HorizontalAlignment HTextAlign { get; set; }
    public VerticalAlignment VTextAlign { get; set; }

    public TextModel(WidgetNode node) : base(node)
    {
    }
  }
}