using System.Drawing;
using OpenTK;
using QuickFont;

namespace Oxy.Framework.Objects
{
  public abstract class FontObject
  {
    internal abstract void Print(QFontDrawing drawing, Color color, string text);
  }
}