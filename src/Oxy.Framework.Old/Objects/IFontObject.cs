using System.Drawing;

namespace Oxy.Framework.Objects
{
  public interface IFontObject
  {
    TextureObject Render(string text, Color color);
  }
}