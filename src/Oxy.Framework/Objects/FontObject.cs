using System.Drawing;
using OpenTK;
using SizeF = System.Drawing.SizeF;

namespace Oxy.Framework.Objects
{
  public abstract class FontObject
  {
    internal abstract void Print(SolidBrush Brush, SizeF size, string text, float x = 0, float y = 0, float r = 0, float sx = 1, float sy = 1);
  }
}