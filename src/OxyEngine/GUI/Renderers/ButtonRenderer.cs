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

    public ButtonState Render(string text, Rectangle rect, Rectangle sourceRect,
      Style style = null)
    {
      style = style ?? GetDefaultStyle();
      var backTexture = style.GetRule<Texture2D>("background-image") ?? EmptyTexture2D;
      
      _imageRenderer.Render(backTexture, rect, sourceRect, style);
      
      return new ButtonState();
    } 
  }
}