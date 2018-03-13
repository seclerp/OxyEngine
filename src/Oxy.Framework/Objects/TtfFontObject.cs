using System;
using System.Drawing;
using OpenTK;
using QuickFont;

namespace Oxy.Framework.Objects
{
  /// <summary>
  ///   Represents font with given name and size
  /// </summary>
  public class TtfFontObject : FontObject, IDisposable
  {
    public TtfFontObject(QFont font, float size)
    {
      _font = font;
      _size = size;
    }

    public void Dispose()
    {
      _font.Dispose();
    }

    private QFont _font;
    private float _size;

    /// <summary>
    ///   Returns size of the font
    /// </summary>
    /// <returns></returns>
    public float GetSize()
    {
      return _size;
    }

    /// <summary>
    ///   Returns name of the font
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
      return _font.FontName;
    }

    internal override void Print(QFontDrawing drawing, Color color, string text)
    {
      drawing.Print(_font, text, new Vector3(0, 0, 0), QFontAlignment.Left, color);
    }
  }
}