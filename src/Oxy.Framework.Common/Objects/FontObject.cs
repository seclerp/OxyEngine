using System;
using System.Drawing;
using Oxy.Framework.Events;

namespace Oxy.Framework.Objects
{
  /// <summary>
  ///   Represents font with given name and size
  /// </summary>
  public class FontObject : IDisposable
  {
    public FontObject(Font font)
    {
      Font = font;
    }

    public Font Font { get; private set; }
    public event EventHandler<FontSizeChangedEventArgs> OnFontSizeChanged;

    /// <summary>
    ///   Set new size for font
    /// </summary>
    /// <param name="newSize">New size in pixels</param>
    public void SetSize(float newSize)
    {
      Font = new Font(Font.FontFamily, newSize);

      OnFontSizeChanged?.Invoke(this, new FontSizeChangedEventArgs {NewSize = newSize});
    }

    /// <summary>
    ///   Returns size of the font
    /// </summary>
    /// <returns></returns>
    public float GetSize()
    {
      return Font.Size;
    }

    public void Dispose()
    {
      Font?.Dispose();
    }
  }
}