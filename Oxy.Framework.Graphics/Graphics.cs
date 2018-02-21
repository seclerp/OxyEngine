using System.Drawing;
using OpenTK;
using QuickFont;
using Color = OpenTK.Color;

namespace Oxy.Framework
{
  /// <summary>
  /// Class that used for rendering all things in engine
  /// </summary>
  public class Graphics : LazyModule<Graphics>
  {
    private const byte DefaultColorR = 255;
    private const byte DefaultColorG = 255;
    private const byte DefaultColorB = 255;
    private const byte DefaultColorA = 255;
    
    private const byte DefaultBgColorR = 100;
    private const byte DefaultBgColorG = 149;
    private const byte DefaultBgColorB = 237;
    private const byte DefaultBgColorA = 255;
    
    private Color _foregroundColor;
    private Color _backgroundColor;

    public Graphics()
    {
      _backgroundColor = Color.FromArgb(DefaultBgColorA, DefaultBgColorR, DefaultBgColorG, DefaultBgColorB);
      _foregroundColor = Color.FromArgb(DefaultColorA, DefaultColorR, DefaultColorG, DefaultColorB);
    }
    
    #region Set

    public static void SetColor(byte r = DefaultColorR, byte g = DefaultColorG, byte b = DefaultColorB, byte a = DefaultColorA) =>
      Instance.Value._foregroundColor = Color.FromArgb(a, r, g, b);
    
    public static void SetBackgroundColor(byte r = DefaultBgColorR, byte g = DefaultBgColorG, byte b = DefaultBgColorB, byte a = DefaultBgColorA) =>
      Instance.Value._backgroundColor = Color.FromArgb(a, r, g, b);
    
    #endregion
    
    #region Get

    public static (byte, byte, byte, byte) GetColor() => 
      (
        Instance.Value._foregroundColor.R, 
        Instance.Value._foregroundColor.G,
        Instance.Value._foregroundColor.B,
        Instance.Value._foregroundColor.A
      );
    
    public static (byte, byte, byte, byte) GetBackgroundColor() =>
      (
        Instance.Value._backgroundColor.R, 
        Instance.Value._backgroundColor.G,
        Instance.Value._backgroundColor.B,
        Instance.Value._backgroundColor.A
      );
    
    #endregion

    public static void Draw(IDrawable drawable, float x = 0, float y = 0, float r = 0, float sx = 1, float sy = 1) =>
      drawable.Draw(x, y, r, sx, sy);
    
    public static TextObject NewText(Font font, string text = "") =>
      new TextObject(font, text);
  }
}