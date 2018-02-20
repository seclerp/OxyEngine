using OpenTK;
using QuickFont;

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
    
    private QFont _currentFont;
    private QFontDrawing _drawing;
    private Color _foregroundColor;
    private Color _backgroundColor;

    public Graphics()
    {
      _backgroundColor = Color.FromArgb(DefaultBgColorA, DefaultBgColorR, DefaultBgColorG, DefaultBgColorB);
      _foregroundColor = Color.FromArgb(DefaultColorA, DefaultColorR, DefaultColorG, DefaultColorB);
      _drawing = new QFontDrawing();
    }
    
    #region Set

    public static void SetFont(QFont font)
    {
      Instance.Value._currentFont = font;
    }
    
    public static void SetColor(byte r = DefaultColorR, byte g = DefaultColorG, byte b = DefaultColorB, byte a = DefaultColorA) =>
      Instance.Value._foregroundColor = Color.FromArgb(a, r, g, b);
    
    public static void SetBackgroundColor(byte r = DefaultBgColorR, byte g = DefaultBgColorG, byte b = DefaultBgColorB, byte a = DefaultBgColorA) =>
      Instance.Value._backgroundColor = Color.FromArgb(a, r, g, b);
    
    #endregion
    
    #region Get

    public static QFont GetFont() =>
      Instance.Value._currentFont;
    
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

    public static void ClearFontDrawing() =>
      Instance.Value._drawing.DrawingPrimitives.Clear();
    
    public static void Print(string text, float x = 0, float y = 0, float r = 0, float sx = 1, float sy = 1) =>
      Instance.Value._drawing.Print(Instance.Value._currentFont, text, new Vector3(x, y, 0), QFontAlignment.Left, Instance.Value._foregroundColor);

    public static void RenderFontDrawing()
    {
      Instance.Value._drawing.RefreshBuffers();
      Instance.Value._drawing.Draw();
    }
      
    
  }
}