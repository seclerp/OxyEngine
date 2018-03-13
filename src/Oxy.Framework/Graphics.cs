using System;
using System.Drawing;
using System.Reflection;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Oxy.Framework.Interfaces;
using Oxy.Framework.Objects;

namespace Oxy.Framework
{
  /// <summary>
  ///   Class that used for rendering all things in engine
  /// </summary>
  public class Graphics : LazyModule<Graphics>
  {
    private const float Deg2Rad = 3.14159f / 180f;
    private const float CircleSegmentK = 2f;

    private const string DefaultFont = "builtin.font.roboto.ttf";
    
    private const byte DefaultColorR = 255;
    private const byte DefaultColorG = 255;
    private const byte DefaultColorB = 255;
    private const byte DefaultColorA = 255;

    private const byte DefaultBgColorR = 100;
    private const byte DefaultBgColorG = 149;
    private const byte DefaultBgColorB = 237;
    private const byte DefaultBgColorA = 255;
    private Color _backgroundColor;

    private Color _foregroundColor;
    private float _lineThickness;
    private TtfFontObject _defaultFont;

    public Graphics()
    {
      _backgroundColor = Color.FromArgb(DefaultBgColorA, DefaultBgColorR, DefaultBgColorG, DefaultBgColorB);
      _foregroundColor = Color.FromArgb(DefaultColorA, DefaultColorR, DefaultColorG, DefaultColorB);
      _lineThickness = 1;

      _defaultFont = Resources.LoadFont(typeof(Graphics).Assembly.GetManifestResourceStream("Oxy.Framework.builtin.font.roboto.ttf"));
    }

    #region Fabrics

    /// <summary>
    ///   Creates new TextObject
    /// </summary>
    /// <param name="ttfFont">Font to be used</param>
    /// <param name="text">Text for printing</param>
    /// <returns></returns>
    public static TextObject NewText(FontObject ttfFont, string text = "")
    {
      return new TextObject(ttfFont, text);
    }

    /// <summary>
    ///   Creates new TextObject using default font (Roboto, 12)
    /// </summary>
    /// <param name="text">Text for printing</param>
    /// <returns></returns>
    public static TextObject NewText(string text = "")
    {
      return NewText(Instance.Value._defaultFont, text);
    }

    /// <summary>
    ///   Creates new RectObject
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="w">Width</param>
    /// <param name="h">Height</param>
    /// <returns></returns>
    public static RectObject NewRect(float x, float y, float w, float h)
    {
      return new RectObject(x, y, w, h);
    }

    #endregion

    #region Set

    /// <summary>
    ///   Set drawing color
    ///   Call without parameters to reset to defaults
    /// </summary>
    /// <param name="r">Red color component</param>
    /// <param name="g">Green color component</param>
    /// <param name="b">Blue color component</param>
    /// <param name="a">Alpha color component</param>
    public static void SetColor(byte r = DefaultColorR, byte g = DefaultColorG, byte b = DefaultColorB,
      byte a = DefaultColorA)
    {
      Instance.Value._foregroundColor = Color.FromArgb(a, r, g, b);
      GL.Color3(Instance.Value._foregroundColor);
    }

    /// <summary>
    ///   Set background color
    ///   Call without parameters to reset default color
    /// </summary>
    /// <param name="r">Red color component</param>
    /// <param name="g">Green color component</param>
    /// <param name="b">Blue color component</param>
    /// <param name="a">Alpha color component</param>
    public static void SetBackgroundColor(byte r = DefaultBgColorR, byte g = DefaultBgColorG, byte b = DefaultBgColorB,
      byte a = DefaultBgColorA)
    {
      Instance.Value._backgroundColor = Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    ///   Set line thickness for drawing primitives
    /// </summary>
    /// <param name="thickness">Thickness (>= 1)</param>
    /// <exception cref="Exception">Fires when thickness is lower than 1</exception>
    public static void SetLineThickness(float thickness)
    {
      if (thickness < 1) throw new Exception("Thickness must be greater or equals 1");
      Instance.Value._lineThickness = thickness;
      GL.LineWidth(thickness);
    }

    #endregion

    #region Get

    /// <summary>
    ///   Returns main drawing color
    /// </summary>
    /// <returns>Tuple with 4 items - R, G, B, A color components</returns>
    public static (byte, byte, byte, byte) GetColor()
    {
      return (
        Instance.Value._foregroundColor.R,
        Instance.Value._foregroundColor.G,
        Instance.Value._foregroundColor.B,
        Instance.Value._foregroundColor.A
        );
    }

    /// <summary>
    ///   Returns background color
    /// </summary>
    /// <returns>Tuple with 4 items - R, G, B, A color components</returns>
    public static (byte, byte, byte, byte) GetBackgroundColor()
    {
      return (
        Instance.Value._backgroundColor.R,
        Instance.Value._backgroundColor.G,
        Instance.Value._backgroundColor.B,
        Instance.Value._backgroundColor.A
        );
    }

    /// <summary>
    ///   Returns primitive's line thickness
    /// </summary>
    /// <returns></returns>
    public static float GetLineThickness()
    {
      return Instance.Value._lineThickness;
    }

    #endregion

    #region Drawing

    /// <summary>
    ///   Draw any drawable object on the screen with given position, rotation and scale
    /// </summary>
    /// <param name="drawable">Object to draw</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="ox">X offset</param>
    /// <param name="oy">Y offset</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public static void Draw(IDrawable drawable, float x = 0, float y = 0, float ox = 0, float oy = 0, float r = 0, float sx = 1, float sy = 1)
    {
      drawable.Draw(x, y, ox, oy, r, sx, sy);
    }

    /// <summary>
    ///   Draw texture on the screen with given quad, position, rotation and scale
    /// </summary>
    /// <param name="texture">Texture to draw</param>
    /// <param name="sourceRect">Texture rectangle</param>
    /// <param name="destRect">Pixel rectangle</param>
    /// <param name="ox">X offset</param>
    /// <param name="oy">Y offset</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public static void Draw(TextureObject texture, RectObject sourceRect, RectObject destRect, float ox = 0, float oy = 0, float r = 0, float sx = 1, float sy = 1)
    {
      texture.Draw(sourceRect, destRect, ox, oy, r, sx, sy);
    }

    /// <summary>
    ///   Draw rectangle
    /// </summary>
    /// <param name="style">"fill" for filled rectangle, "line" for outilne rectangle</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="width">Width of rectangle</param>
    /// <param name="height">Height of rectangle</param>
    public static void DrawRectangle(string style, float x, float y, float width, float height)
    {
      if (style == "line")
      {
        float th = Instance.Value._lineThickness / 2;

        GL.Begin(PrimitiveType.Lines);

        GL.Vertex3(x + width, y - th, 0.1);
        GL.Vertex3(x + width, y + height + th, 0);

        GL.Vertex3(x - th, y, 0.1);
        GL.Vertex3(x + width + th, y, 0.1);

        GL.Vertex3(x, y - th, 0.1);
        GL.Vertex3(x, y + height + th, 0);

        GL.Vertex3(x - th, y + height, 0);
        GL.Vertex3(x + width + th, y + height, 0);

        GL.End();
      }
      else if (style == "fill")
      {
        GL.Begin(PrimitiveType.Quads);

        GL.Vertex3(x + width, y + height, 0);
        GL.Vertex3(x + width, y, 0.1);
        GL.Vertex3(x, y, 0.1);
        GL.Vertex3(x, y + height, 0);

        GL.End();
      }
      else
      {
        throw new Exception($"Incorrect style for drawing: {style}");
      }
    }

    /// <summary>
    ///   Draw rectangle
    /// </summary>
    /// <param name="style">"fill" for filled rectangle, "line" for outilne rectangle</param>
    /// <param name="rect">RectObject with X, Y, Width, Height</param>
    public static void DrawRectangle(string style, RectObject rect)
    {
      DrawRectangle(style, rect.X, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    ///   Draw circle
    /// </summary>
    /// <param name="style">"fill" for filled rectangle, "line" for outilne rectangle</param>
    /// <param name="x">X coordinate of center</param>
    /// <param name="y">Y coordinate of center</param>
    /// <param name="radius">Radius of circle</param>
    public static void DrawCircle(string style, float x, float y, float radius)
    {
      if (style == "line")
      {
        GL.Begin(PrimitiveType.Lines);

        float segments = (int) Math.Ceiling(radius / 3) * 3;

        for (float i = 1; i <= 360; i += 360 / segments)
        {
          var rad = i * Deg2Rad;
          var radPrev = (i - 360 / segments) * Deg2Rad;

          GL.Vertex3((int) (x + Math.Cos(radPrev) * radius), (int) (y + Math.Sin(radPrev) * radius), 0);
          GL.Vertex3((int) (x + Math.Cos(rad) * radius), (int) (y + Math.Sin(rad) * radius), 0);
        }

        GL.End();
      }
      else if (style == "fill")
      {
        GL.Begin(PrimitiveType.Triangles);

        float segments = (int) Math.Ceiling(radius / 3) * 3;

        for (float i = 1; i <= 360; i += 360 / segments)
        {
          var rad = i * Deg2Rad;
          var radPrev = (i - 360 / segments) * Deg2Rad;

          GL.Vertex3(x, y, 0);
          GL.Vertex3((int) (x + Math.Cos(radPrev) * radius), (int) (y + Math.Sin(radPrev) * radius), 0);
          GL.Vertex3((int) (x + Math.Cos(rad) * radius), (int) (y + Math.Sin(rad) * radius), 0);
        }

        GL.End();
      }
      else
      {
        throw new Exception($"Incorrect style for drawing: '{style}'");
      }
    }

    /// <summary>
    ///   Draw polygon
    /// </summary>
    /// <param name="style"></param>
    /// <param name="xy"></param>
    public static void DrawPolygon(string style, params float[] xy)
    {
      if (xy.Length % 2 != 0)
        throw new Exception("Incorrect count of coordinates: they must be in pairs (x, y)");

      if (style == "line")
      {
        GL.Begin(PrimitiveType.Lines);

        for (int i = 2; i < xy.Length; i += 2)
        {
          GL.Vertex3(xy[i - 2], xy[i - 1], 0);
          GL.Vertex3(xy[i], xy[i + 1], 0);
        }

        GL.Vertex3(xy[0], xy[1], 0);
        GL.Vertex3(xy[xy.Length - 2], xy[xy.Length - 1], 0);

        GL.End();
      }
      else if (style == "fill")
      {
        GL.Begin(PrimitiveType.Polygon);

        for (int i = 0; i < xy.Length; i += 2) GL.Vertex3(xy[i], xy[i + 1], 0);

        GL.End();
      }
      else
      {
        throw new Exception($"Incorrect style for drawing: '{style}'");
      }
    }

    /// <summary>
    ///   Draw line
    /// </summary>
    /// <param name="x1">X coordinate of first point</param>
    /// <param name="y1">Y coordinate of first point</param>
    /// <param name="x2">X coordinate of second point</param>
    /// <param name="y2">Y coordinate of second point</param>
    public static void DrawLine(float x1, float y1, float x2, float y2)
    {
      GL.Begin(PrimitiveType.Lines);

      GL.Vertex3(x1, y1, 0);
      GL.Vertex3(x2, y2, 0);

      GL.End();
    }

    /// <summary>
    ///   Draw point
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public static void DrawPoint(float x, float y)
    {
      float th = Instance.Value._lineThickness / 2;

      GL.Begin(PrimitiveType.Quads);

      GL.Vertex3(x + th, y + th, 0);
      GL.Vertex3(x + th, y - th, 0.1);
      GL.Vertex3(x - th, y - th, 0.1);
      GL.Vertex3(x - th, y + th, 0);

      GL.End();
    }

    #endregion

    #region Matrix transformations

    /// <summary>
    ///   Pushes matrix stack
    /// </summary>
    public static void PushMatrix()
    {
      GL.PushMatrix();
    }

    /// <summary>
    ///   Pops matrix stack
    /// </summary>
    public static void PopMatrix()
    {
      GL.PushMatrix();
    }

    /// <summary>
    ///   Moves coordinate system among X and Y axis
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public static void Translate(float x, float y)
    {
      GL.Translate(x, y, 0);
    }

    /// <summary>
    ///   Rotates coordinate system by given angle
    /// </summary>
    /// <param name="r">Angle to rotate</param>
    public static void Rotate(float r)
    {
      GL.Rotate(r, Vector3.UnitZ);
    }

    /// <summary>
    ///   Scales coordinate system
    /// </summary>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public static void Scale(float sx, float sy)
    {
      GL.Scale(sx, sy, 1);
    }

    /// <summary>
    ///   Scales coordinate system
    /// </summary>
    /// <param name="s">X and Y scale factor</param>
    public static void Scale(float s)
    {
      GL.Scale(s, s, 1);
    }

    #endregion
  }
}