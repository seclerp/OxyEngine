using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Oxy.Framework.Interfaces;
using Bitmap = System.Drawing.Bitmap;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using Rectangle = System.Drawing.Rectangle;

namespace Oxy.Framework.Objects
{
  /// <summary>
  ///   Object for drawing textures on screen
  /// </summary>
  /// <seealso cref="IDrawable" />
  /// <seealso cref="System.IDisposable" />
  public class TextureObject : IDrawable, IDisposable
  {
    public int Width => _texture.Width;
    public int Height => _texture.Height;

    public int Id { get; }
    private readonly Bitmap _texture;
    private string _wrapModeX;
    private string _wrapModeY;

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextureObject" /> class.
    /// </summary>
    /// <param name="texture">The texture.</param>
    /// <exception cref="ArgumentNullException">texture</exception>
    internal TextureObject(Bitmap texture)
    {
      _texture = texture ?? throw new ArgumentNullException(nameof(texture));

      Id = LoadToGpu();
    }

    public void SetWrap(string wrapModeX, string wrapModeY)
    {
      _wrapModeX = wrapModeX;
      _wrapModeY = wrapModeY;

      GL.BindTexture(TextureTarget.Texture2D, Id);

      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, wrapModeX == "repeat" ? (int) All.Repeat : (int) All.ClampToEdge);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, wrapModeY == "repeat" ? (int) All.Repeat : (int) All.ClampToEdge);

      GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    public string GetWrapX()
    {
      return _wrapModeX;
    }

    public string GetWrapY()
    {
      return _wrapModeY;
    }

    public RectObject GetUV(RectObject original)
    {
      return GetUV(original.X, original.Y, original.Width, original.Height);
    }

    public RectObject GetUV(float x, float y, float w, float h)
    {
      return new RectObject(
        x / _texture.Width, y / _texture.Height,
        w / _texture.Width, h / _texture.Height
      );
    }

    /// <summary>
    ///   Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      _texture.Dispose();
    }

    /// <summary>
    ///   Draws this object on screen with given position, rotation and scale
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="ox">X offset</param>
    /// <param name="oy">Y offset</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Draw(float x, float y, float ox, float oy, float r, float sx, float sy)
    {
      GL.Translate(x + ox, y + oy, 0);
      GL.Rotate(r, Vector3.UnitZ);
      GL.Scale(sx, sy, 1);

      GL.BindTexture(TextureTarget.Texture2D, Id);
      GL.Begin(PrimitiveType.Quads);

      GL.TexCoord2(0, 0); GL.Vertex3(-ox, -oy, 0);
      GL.TexCoord2(0, 1); GL.Vertex3(-ox, _texture.Height - oy, 0);
      GL.TexCoord2(1, 1); GL.Vertex3(_texture.Width - ox, _texture.Height - oy, 0);
      GL.TexCoord2(1, 0); GL.Vertex3(_texture.Width - ox, -oy, 0);

      GL.End();
      GL.BindTexture(TextureTarget.Texture2D, 0);

      GL.Scale(1 / sx, 1 / sy, 1);
      GL.Rotate(-r, Vector3.UnitZ);
      GL.Translate(-x - ox, -y - oy, 0);
    }

    /// <summary>
    ///   Draws this object on screen with given position, rotation and scale
    /// </summary>
    /// <param name="sourceRect"></param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="ox">X offset</param>
    /// <param name="oy">Y offset</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Draw(RectObject sourceRect, RectObject destRect, float ox, float oy, float r, float sx, float sy)
    {
      GL.Translate(destRect.X + ox, destRect.Y + oy, 0);
      GL.Rotate(r, Vector3.UnitZ);
      GL.Scale(sx, sy, 1);

      GL.BindTexture(TextureTarget.Texture2D, Id);
      GL.Begin(PrimitiveType.Quads);

      GL.TexCoord2(sourceRect.X, sourceRect.Y);                                        GL.Vertex3(-ox, -oy, 0);
      GL.TexCoord2(sourceRect.X, sourceRect.Y + sourceRect.Height);                    GL.Vertex3(-ox, -oy + destRect.Height, 0);
      GL.TexCoord2(sourceRect.X + sourceRect.Width, sourceRect.Y + sourceRect.Height); GL.Vertex3(-ox + destRect.Width, -oy + destRect.Height, 0);
      GL.TexCoord2(sourceRect.X + sourceRect.Width, sourceRect.Y);                     GL.Vertex3(-ox + destRect.Width, -oy, 0);

      GL.End();
      GL.BindTexture(TextureTarget.Texture2D, 0);
      
      GL.Scale(1/sx, 1/sy, 1);
      GL.Rotate(-r, Vector3.UnitZ);
      GL.Translate(-destRect.X - ox, -destRect.Y - oy, 0);
    }

    private int LoadToGpu(int quality = 0, bool repeat = true, bool flipY = false)
    {
      if (flipY)
        _texture.RotateFlip(RotateFlipType.RotateNoneFlipY);

      int texture = GL.GenTexture();

      GL.BindTexture(TextureTarget.Texture2D, texture);

      switch (quality)
      {
        case 0:
        default:
          GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) All.Linear);
          GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) All.Linear);
          break;
        case 1: 
          GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) All.Nearest);
          GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) All.Nearest);
          break;
      }

      if (repeat)
      {
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) All.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) All.Repeat);
      }
      else
      {
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) All.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) All.ClampToEdge);
      }

      /* Parameters
       * Target - Since we are using a 2D image we specify the target Texture2D
       * MipMap Count / LOD - 0 as we are not using mipmapping at the moment
       * InternalFormat - The format of the gl texture, Rgba is a base format it works all around
       * Width;
       * Height;
       * Border - must be 0;
       * 
       * Format - this is the images format not gl's the format Bgra i believe is only language specific
       *          C# uses little-endian so you have ARGB on the image A 24 R 16 G 8 B, B is the lowest
       *          So it gets counted first, as with a language like Java it would be PixelFormat.Rgba
       *          since Java is big-endian default meaning A is counted first.
       *          but i could be wrong here it could be cpu specific :P
       *          
       * PixelType - The type we are using, eh in short UnsignedByte will just fill each 8 bit till the pixelformat is full
       *             (don't quote me on that...)
       *             you can be more specific and say for are RGBA to little-endian BGRA -> PixelType.UnsignedInt8888Reversed
       *             this will mimic are 32bit uint in little-endian.
       *             
       * Data - No data at the moment it will be written with TexSubImage2D
       */
      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, _texture.Width, _texture.Height, 0,
        PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);

      BitmapData bitmap_data = _texture.LockBits(new Rectangle(0, 0, _texture.Width, _texture.Height),
        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      /* Target;
       * MipMap;
       * X Offset - Offset of the data on the x axis
       * Y Offset - Offset of the data on the y axis
       * Width;
       * Height;
       * Format;
       * Type;
       * Data - Now we have data from the loaded bitmap image we can load it into are texture data
       */
      GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, _texture.Width, _texture.Height, PixelFormat.Bgra,
        PixelType.UnsignedByte, bitmap_data.Scan0);

      _texture.UnlockBits(bitmap_data);


      GL.BindTexture(TextureTarget.Texture2D, 0);

      return texture;
    }
  }
}