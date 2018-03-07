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
    public int Height => _texture.Width;
    
    private readonly int _id;
    private readonly Bitmap _texture;

    /// <summary>
    ///   Initializes a new instance of the <see cref="TextureObject" /> class.
    /// </summary>
    /// <param name="texture">The texture.</param>
    /// <exception cref="ArgumentNullException">texture</exception>
    public TextureObject(Bitmap texture)
    {
      _texture = texture ?? throw new ArgumentNullException(nameof(texture));

      _id = LoadToGpu();
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
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    /// <exception cref="NotImplementedException"></exception>
    public void Draw(float x, float y, float r, float sx, float sy)
    {
      GL.Translate(x, y, 0);
      GL.Rotate(r, Vector3.UnitZ);
      GL.Scale(sx, sy, 1);

      GL.BindTexture(TextureTarget.Texture2D, _id);
      GL.Begin(PrimitiveType.Quads);

      GL.TexCoord2(0, 0);
      GL.Vertex3(x, y, 0);
      GL.TexCoord2(0, 1);
      GL.Vertex3(x, y + _texture.Height, 0);
      GL.TexCoord2(1, 1);
      GL.Vertex3(x + _texture.Width, y + _texture.Height, 0);
      GL.TexCoord2(1, 0);
      GL.Vertex3(x + _texture.Width, y, 0);

      GL.End();
      GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    private int LoadToGpu(int quality = 0, bool repeat = true, bool flip_y = false)
    {
      //Flip the image
      if (flip_y)
        _texture.RotateFlip(RotateFlipType.RotateNoneFlipY);

      //Generate a new texture target in gl
      int texture = GL.GenTexture();

      //Will bind the texture newly/empty created with GL.GenTexture
      //All gl texture methods targeting Texture2D will relate to this texture
      GL.BindTexture(TextureTarget.Texture2D, texture);

      //The reason why your texture will show up glColor without setting these parameters is actually
      //TextureMinFilters fault as its default is NearestMipmapLinear but we have not established mipmapping
      //We are only using one texture at the moment since mipmapping is a collection of textures pre filtered
      //I'm assuming it stops after not having a collection to check.
      switch (quality)
      {
        case 0:
        default: //Low quality
          GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) All.Linear);
          GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) All.Linear);
          break;
        case 1: //High quality
          //This is in my opinion the best since it doesnt average the result and not blurred to shit
          //but most consider this low quality...
          GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) All.Nearest);
          GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) All.Nearest);
          break;
      }

      if (repeat)
      {
        //This will repeat the texture past its bounds set by TexImage2D
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) All.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) All.Repeat);
      }
      else
      {
        //This will clamp the texture to the edge, so manipulation will result in skewing
        //It can also be useful for getting rid of repeating texture bits at the borders
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int) All.ClampToEdge);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int) All.ClampToEdge);
      }

      //Creates a definition of a texture object in opengl
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

      //Load the data from are loaded image into virtual memory so it can be read at runtime
      BitmapData bitmap_data = _texture.LockBits(new Rectangle(0, 0, _texture.Width, _texture.Height),
        ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      //Writes data to are texture target
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

      //Release from memory
      _texture.UnlockBits(bitmap_data);

      /*Binding to 0 is telling gl to use the default or null texture target
      *This is useful to remember as you may forget that a texture is targeted
      *And may overflow to functions that you dont necessarily want to
      *Say you bind a texture
      *
      * Bind(Texture);
      * DrawObject1();
      *                <-- Insert Bind(NewTexture) or Bind(0)
      * DrawObject2();
      * 
      * Object2 will use Texture if not set to 0 or another.
      */
      GL.BindTexture(TextureTarget.Texture2D, 0);

      return texture;
    }
  }
}