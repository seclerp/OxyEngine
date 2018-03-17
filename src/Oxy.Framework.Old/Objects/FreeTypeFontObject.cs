using System;
using System.Drawing;
using Examples;
using OpenTK;
using SharpFont;

namespace Oxy.Framework.Objects
{
  /// <summary>
  ///   Represents font with given name and size
  /// </summary>
  public class FreeTypeFontObject : IFontObject, IDisposable
  {
    public FreeTypeFontObject(string fontFile, float size)
    {
      _ttfFreeTypeFontService = new FreeTypeFontService();
      _ttfFreeTypeFontService.SetFont(fontFile);
      _ttfFreeTypeFontService.SetSize(size * 2);
    }

    public FreeTypeFontObject(byte[] fontRaw, float size)
    {
      _ttfFreeTypeFontService = new FreeTypeFontService();
      _ttfFreeTypeFontService.SetFont(fontRaw);
      _ttfFreeTypeFontService.SetSize(size * 2);
    }

    public void Dispose()
    {
      _ttfFreeTypeFontService.Dispose();
    }

    private FreeTypeFontService _ttfFreeTypeFontService;

    /// <summary>
    ///   Returns size of the font
    /// </summary>
    /// <returns></returns>
    public float GetSize()
    {
      return _ttfFreeTypeFontService.Size;
    }

    /// <summary>
    ///   Returns name of the font
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
      return _ttfFreeTypeFontService.FontFace.FamilyName;
    }

    public TextureObject Render(string text, Color color)
    {
      return new TextureObject(_ttfFreeTypeFontService.RenderString(text, color));
    }
  }
}