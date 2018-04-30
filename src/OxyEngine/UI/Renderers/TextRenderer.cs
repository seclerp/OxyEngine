using OxyEngine.UI.Widgets;

namespace OxyEngine.UI.Renderers
{
  public class TextRenderer : WidgetRenderer
  {
    private Text _text;
    
    public TextRenderer(Text text)
    {
      _text = text;
    }

    public override void Render()
    {
      var beforeColor = GraphicsApi.GetColor();
      GraphicsApi.SetColor(_text.BackgroundColor.R, _text.BackgroundColor.G, _text.BackgroundColor.B, _text.BackgroundColor.A);
      GraphicsApi.Rectangle("fill", (int) _text.X, (int) _text.Y, (int) _text.Width, (int) _text.Height);
      var beforeFont = GraphicsApi.GetFont();
      GraphicsApi.SetFont(_text.Font);
      GraphicsApi.SetColor(_text.ForegroundColor.R, _text.ForegroundColor.G, _text.ForegroundColor.B, _text.ForegroundColor.A);
      GraphicsApi.Print(_text.Value);
      GraphicsApi.SetFont(beforeFont);
      GraphicsApi.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
    }
  }
}