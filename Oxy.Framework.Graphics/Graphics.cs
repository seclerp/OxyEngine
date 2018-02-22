using System.Drawing;
using OpenTK;
using OpenTK.Audio.OpenAL;
using OpenTK.Graphics.OpenGL;
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

    /// <summary>
    /// Set drawing color
    /// Call without parameters to reset to defaults
    /// </summary>
    /// <param name="r">Red color component</param>
    /// <param name="g">Green color component</param>
    /// <param name="b">Blue color component</param>
    /// <param name="a">Alpha color component</param>
    public static void SetColor(byte r = DefaultColorR, byte g = DefaultColorG, byte b = DefaultColorB, byte a = DefaultColorA) =>
      Instance.Value._foregroundColor = Color.FromArgb(a, r, g, b);
    
    /// <summary>
    /// Set background color
    /// Call without parameters to reset default color
    /// </summary>
    /// <param name="r">Red color component</param>
    /// <param name="g">Green color component</param>
    /// <param name="b">Blue color component</param>
    /// <param name="a">Alpha color component</param>
    public static void SetBackgroundColor(byte r = DefaultBgColorR, byte g = DefaultBgColorG, byte b = DefaultBgColorB, byte a = DefaultBgColorA) =>
      Instance.Value._backgroundColor = Color.FromArgb(a, r, g, b);
    
    #endregion
    
    #region Get

    /// <summary>
    /// Returns main drawing color
    /// </summary>
    /// <returns>Tuple with 4 items - R, G, B, A color components</returns>
    public static (byte, byte, byte, byte) GetColor() => 
      (
        Instance.Value._foregroundColor.R, 
        Instance.Value._foregroundColor.G,
        Instance.Value._foregroundColor.B,
        Instance.Value._foregroundColor.A
      );
    
    /// <summary>
    /// Returns background color
    /// </summary>
    /// <returns>Tuple with 4 items - R, G, B, A color components</returns>   
    public static (byte, byte, byte, byte) GetBackgroundColor() =>
      (
        Instance.Value._backgroundColor.R, 
        Instance.Value._backgroundColor.G,
        Instance.Value._backgroundColor.B,
        Instance.Value._backgroundColor.A
      );
    
    #endregion

    #region Drawing
    
    /// <summary>
    /// Draw any drawable object on the screen with given position, rotation and scale
    /// </summary>
    /// <param name="drawable">Object to draw</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public static void Draw(IDrawable drawable, float x = 0, float y = 0, float r = 0, float sx = 1, float sy = 1) =>
      drawable.Draw(x, y, r, sx, sy);
    
    #endregion 
    
    #region Matrix transformations

    /// <summary>
    /// Pushes matrix stack
    /// </summary>
    public static void PushMatrix() =>
      GL.PushMatrix();
    
    /// <summary>
    /// Pops matrix stack
    /// </summary>
    public static void PopMatrix() =>
      GL.PushMatrix();

    /// <summary>
    /// Moves coordinate system among X and Y axis
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public static void Translate(float x, float y) =>
      GL.Translate(x, y, 0);

    /// <summary>
    /// Rotates coordinate system by given angle
    /// </summary>
    /// <param name="r">Angle to rotate</param>
    public static void Rotate(float r) =>
      GL.Rotate(r, Vector3.UnitZ);

    /// <summary>
    /// Scales coordinate system 
    /// </summary>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public static void Scale(float sx, float sy) =>
      GL.Scale(sx, sy, 1);
    
    /// <summary>
    /// Scales coordinate system 
    /// </summary>
    /// <param name="s">X and Y scale factor</param>
    public static void Scale(float s) =>
      GL.Scale(s, s, 1);
    
    #endregion
    
    #region Fabrics
    
    /// <summary>
    /// Creates new TextObject
    /// </summary>
    /// <param name="font">Font to be used</param>
    /// <param name="text">Text for printing</param>
    /// <returns></returns>
    public static TextObject NewText(FontObject font, string text = "") =>
      new TextObject(font, text);
    
    #endregion
  }
}