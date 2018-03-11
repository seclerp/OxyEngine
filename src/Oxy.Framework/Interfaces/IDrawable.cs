using Oxy.Framework.Objects;

namespace Oxy.Framework.Interfaces
{
  /// <summary>
  ///   Interface for any object that can be drawn on screen
  /// </summary>
  public interface IDrawable
  {
    /// <summary>
    ///   Draws this object on screen with given position, rotation and scale
    /// </summary>
    /// <param name="sourceRect"></param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="ox"></param>
    /// <param name="oy"></param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    void Draw(RectObject sourceRect, float x, float y, float ox, float oy, float r, float sx, float sy);
  }
}