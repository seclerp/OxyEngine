using System;
using System.Drawing;

namespace Oxy.Framework.Objects
{
  /// <summary>
  /// Object for drawing textures on screen
  /// </summary>
  /// <seealso cref="Oxy.Framework.Objects.IDrawable" />
  /// <seealso cref="System.IDisposable" />
  public class TextureObject : IDrawable, IDisposable
  {
    private Bitmap _texture;

    /// <summary>
    /// Initializes a new instance of the <see cref="TextureObject" /> class.
    /// </summary>
    /// <param name="texture">The texture.</param>
    /// <exception cref="ArgumentNullException">texture</exception>
    public TextureObject(Bitmap texture)
    {
      _texture = texture ?? throw new ArgumentNullException(nameof(texture));
    }

    /// <summary>
    /// Draws this object on screen with given position, rotation and scale
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    /// <exception cref="NotImplementedException"></exception>
    public void Draw(float x, float y, float r, float sx, float sy)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
      => _texture.Dispose();
  }
}
