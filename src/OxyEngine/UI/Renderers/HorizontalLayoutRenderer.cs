using System.Runtime.Remoting.Channels;
using Microsoft.Xna.Framework;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.Renderers
{
  public class HorizontalLayoutRenderer : WidgetRenderer
  {
    private HorizontalLayoutModel _widget;
    
    public HorizontalLayoutRenderer(WidgetNode node) : base(node)
    {
      _widget = node.Model as HorizontalLayoutModel;
    }
    
    public override void Render()
    {
      GraphicsApi.Translate(_widget.ClientRectangle.X, _widget.ClientRectangle.Y);

      var uiCursor = 0f;
      
      foreach (var child in _widget.Children)
      {
        Vector2 finalPosition = new Vector2(), 
                finalSize = new Vector2();

        switch (_widget.VAlign)
        {
          case VerticalAlignment.FullHeight:
            finalSize = new Vector2(child.Width, _widget.ClientRectangle.Height);
            finalPosition = new Vector2(uiCursor, child.MarginTop);
            break;
          case VerticalAlignment.Top:
            finalSize = child.Size;
            finalPosition = new Vector2(uiCursor, child.MarginTop);
            break;
          case VerticalAlignment.Middle:
            finalSize = child.Size;
            finalPosition = new Vector2(uiCursor, (_widget.ClientRectangle.Height - child.Height) / 2 + child.MarginTop);
            break;
          case VerticalAlignment.Bottom:
            finalSize = child.Size;
            finalPosition = new Vector2(uiCursor, _widget.ClientRectangle.Height - child.Size.Y - child.MarginBottom);
            break;
        }

        child.Size = finalSize;
        child.Position = finalPosition;

        child.Node.Renderer.Render();
        
        uiCursor += child.FullSize.X;
      }
    }
  }
}