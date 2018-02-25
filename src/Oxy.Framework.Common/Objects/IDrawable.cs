namespace Oxy.Framework.Objects
{
  /// <summary>
  /// Interface for any object that can be drawn on screen
  /// </summary>
  public interface IDrawable
  {
    /// <summary>
    /// Draws this object on screen with given position, rotation and scale
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    void Draw(float x, float y, float r, float sx, float sy);
  }
}