using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.GUI.States;
using OxyEngine.GUI.Styles;

namespace OxyEngine.GUI.Renderers
{
  public class ButtonRenderer : Renderer
  {
    private TextRenderer _textRenderer;
    private ImageRenderer _imageRenderer;
    
    public ButtonRenderer(AreaStack areaStack, StyleDatabase styles) : base(areaStack, styles)
    {
      _textRenderer = new TextRenderer(areaStack, styles);
      _imageRenderer = new ImageRenderer(areaStack, styles);
    }

    public ButtonState Render(string text, Rectangle rect, Style style)
    {
      //_imageRenderer.Render(backTexture, rect, style);
      
      return new ButtonState();
    } 
  }
}