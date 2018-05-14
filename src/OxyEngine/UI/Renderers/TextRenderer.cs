using System.Runtime.Serialization.Formatters;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.Renderers
{
  public class TextRenderer : WidgetRenderer
  {
    private TextModel _widget;
    
    public TextRenderer(WidgetNode node) : base(node)
    {
      Node = node;
      
      _widget = Node.Model as TextModel;
    }

    public override void Render()
    {
      var beforeColor = GraphicsApi.GetColor();
      GraphicsApi.SetColor(_widget.BackgroundColor.R, _widget.BackgroundColor.G, _widget.BackgroundColor.B, _widget.BackgroundColor.A);
      GraphicsApi.Rectangle("fill", (int) _widget.X, (int) _widget.Y, (int) _widget.Width, (int) _widget.Height);
      
      var beforeFont = GraphicsApi.GetFont();
      GraphicsApi.SetFont(_widget.Font);
      GraphicsApi.SetColor(_widget.ForegroundColor.R, _widget.ForegroundColor.G, _widget.ForegroundColor.B, _widget.ForegroundColor.A);

      var textSize = _widget.Font.MeasureString(_widget.Value);
      var finalX = 0f;
      var finalY = 0f;
      
      switch (_widget.HTextAlign)
      {
        case HorizontalAlignment.Left:
          finalX = _widget.PaddingLeft;
          break;
        case HorizontalAlignment.Center:
          finalX = (_widget.Size.X - _widget.PaddingLeft - _widget.PaddingRight - textSize.X) / 2;
          break;
        case HorizontalAlignment.Right:
          finalX = _widget.Size.X - _widget.PaddingRight - textSize.X;
          break;
      }
      
      switch (_widget.VTextAlign)
      {
        case VerticalAlignment.Top:
          finalY = _widget.PaddingTop;
          break;
        case VerticalAlignment.Middle:
          finalY = (_widget.Size.Y - _widget.PaddingTop - _widget.PaddingBottom - textSize.Y) / 2;
          break;
        case VerticalAlignment.Bottom:
          finalY = _widget.Size.Y - _widget.PaddingBottom - textSize.Y;
          break;
      }
      
      GraphicsApi.Print(_widget.Value, finalX, finalY);

      GraphicsApi.SetFont(beforeFont);
      GraphicsApi.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
    }
  }
}