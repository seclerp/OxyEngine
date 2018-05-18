namespace OxyEngine.UI.Models
{
  public struct Offset
  {
    public readonly float Top;
    public readonly float Left;
    public readonly float Right;
    public readonly float Bottom;

    public Offset(float top, float left, float right, float bottom)
    {
      Top = top;
      Left = left;
      Right = right;
      Bottom = bottom;
    }
  }
}