using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Styles;

namespace OxyEngine.UI.Renderers
{
  public class ImageRenderer : Renderer
  {
    public ImageRenderer(AreaStack areaStack, StyleDatabase styles) : base(areaStack, styles)
    {
    }

    public void Render(Texture2D texture, Rectangle rect, Rectangle sourceRect, Style style = null)
    {
      style = style ?? GetDefaultStyle();
      
      var backColorValue = style.GetRule<Color>("background-color");
      
      var beforeColor = GraphicsManager.GetColor();
      GraphicsManager.SetColor(backColorValue.R, backColorValue.G, backColorValue.B, backColorValue.A);
      GraphicsManager.Rectangle("fill", rect.X, rect.Y, rect.Width, rect.Height);
    }
  }
}