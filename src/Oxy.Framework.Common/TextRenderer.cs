using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Oxy.Framework
{
  /// <summary>
  ///   Uses System.Drawing for 2d text rendering.
  ///   Do not use this class explicitly. Use TextObject instead
  /// </summary>
  public class TextRenderer : IDisposable
  {
    private readonly Bitmap _bitMap;
    private Rectangle _dirtyRegion;
    private bool _disposed;
    private readonly Graphics _graphics;
    private readonly int _texture;

    #region Constructors

    /// <summary>
    ///   Constructs a new instance.
    /// </summary>
    /// <param name="width">The width of the backing store in pixels.</param>
    /// <param name="height">The height of the backing store in pixels.</param>
    public TextRenderer(int width, int height)
    {
      if (width <= 0)
        throw new ArgumentOutOfRangeException("width");
      if (height <= 0)
        throw new ArgumentOutOfRangeException("height");
      if (GraphicsContext.CurrentContext == null)
        throw new InvalidOperationException("No GraphicsContext is current on the calling thread.");

      _bitMap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
      _graphics = Graphics.FromImage(_bitMap);
      _graphics.SmoothingMode = SmoothingMode.HighQuality;
      _graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

      _texture = GL.GenTexture();
      GL.BindTexture(TextureTarget.Texture2D, _texture);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Nearest);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMagFilter.Nearest);
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
        OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
    }

    #endregion

    #region Private Members

    // Uploads the dirty regions of the backing store to the OpenGL texture.
    private void UploadBitmap()
    {
      if (_dirtyRegion != RectangleF.Empty)
      {
        BitmapData data = _bitMap.LockBits(_dirtyRegion,
          ImageLockMode.ReadOnly,
          PixelFormat.Format32bppArgb);

        GL.BindTexture(TextureTarget.Texture2D, _texture);
        GL.TexSubImage2D(TextureTarget.Texture2D, 0,
          _dirtyRegion.X, _dirtyRegion.Y, _dirtyRegion.Width, _dirtyRegion.Height,
          OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

        _bitMap.UnlockBits(data);

        _dirtyRegion = Rectangle.Empty;
      }
    }

    #endregion

    #region Public Members

    /// <summary>
    ///   Clears the backing store to the specified color.
    /// </summary>
    /// <param name="color">A <see cref="System.Drawing.Color" />.</param>
    public void Clear(Color color)
    {
      _graphics.Clear(color);
      _dirtyRegion = new Rectangle(0, 0, _bitMap.Width, _bitMap.Height);
    }

    public SizeF GetStringSize(string text, Font font)
    {
      return _graphics.MeasureString(text, font);
    }

    /// <summary>
    ///   Draws the specified string to the backing store.
    /// </summary>
    /// <param name="text">The <see cref="System.String" /> to draw.</param>
    /// <param name="font">The <see cref="System.Drawing.Font" /> that will be used.</param>
    /// <param name="brush">The <see cref="System.Drawing.Brush" /> that will be used.</param>
    /// <param name="point">
    ///   The location of the text on the backing store, in 2d pixel coordinates.
    ///   The origin (0, 0) lies at the top-left corner of the backing store.
    /// </param>
    public void DrawString(string text, Font font, Brush brush, PointF point)
    {
      _graphics.DrawString(text, font, brush, point);

      var size = _graphics.MeasureString(text, font);
      _dirtyRegion = Rectangle.Round(RectangleF.Union(_dirtyRegion, new RectangleF(point, size)));
      _dirtyRegion = Rectangle.Intersect(_dirtyRegion, new Rectangle(0, 0, _bitMap.Width, _bitMap.Height));
    }

    /// <summary>
    ///   Gets a <see cref="System.Int32" /> that represents an OpenGL 2d texture handle.
    ///   The texture contains a copy of the backing store. Bind this texture to TextureTarget.Texture2d
    ///   in order to render the drawn text on screen.
    /// </summary>
    public int Texture
    {
      get
      {
        UploadBitmap();
        return _texture;
      }
    }

    #endregion

    #region IDisposable Members

    private void Dispose(bool manual)
    {
      if (!_disposed)
      {
        if (manual)
        {
          _bitMap.Dispose();
          _graphics.Dispose();
          if (GraphicsContext.CurrentContext != null)
            GL.DeleteTexture(_texture);
        }

        _disposed = true;
      }
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~TextRenderer()
    {
      Console.WriteLine("[Warning] Resource leaked: {0}.", typeof(TextRenderer));
    }

    #endregion
  }
}