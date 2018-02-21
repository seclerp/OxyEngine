using System;
using System.Drawing;
using Oxy.Framework.Events;

namespace Oxy.Framework
{
  /// <summary>
  /// Represents font with given name and size
  /// </summary>
  public class FontObject : IDisposable
  {
    public Font Font { get; private set; }
    public event EventHandler<FontSizeChangedEventArgs> OnFontSizeChanged;
    
    public FontObject(Font font) =>
      Font = font;

    /// <summary>
    /// Set new size for font
    /// </summary>
    /// <param name="newSize">New size in pixels</param>
    public void SetSize(float newSize)
    {
      Font = new Font(Font.FontFamily, newSize);
      
      OnFontSizeChanged?.Invoke(this, new FontSizeChangedEventArgs { NewSize = newSize });
    }
    
    /// <summary>
    /// Returns size of the font
    /// </summary>
    /// <returns></returns>
    public float GetSize() =>
      Font.Size;

    public void Dispose() =>
      Font?.Dispose();
  }
}