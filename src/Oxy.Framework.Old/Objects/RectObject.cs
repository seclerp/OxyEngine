namespace Oxy.Framework.Objects
{
  public struct RectObject
  {
    public readonly float X;
    public readonly float Y;
    public readonly float Width;
    public readonly float Height;

    internal RectObject(float x, float y, float width, float height)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
    }
  }
}