namespace OxyEngine.GUI.Models
{
  public struct Offset
  {
    public readonly int Top;
    public readonly int Left;
    public readonly int Right;
    public readonly int Bottom;

    public Offset(int top, int left, int right, int bottom)
    {
      Top = top;
      Left = left;
      Right = right;
      Bottom = bottom;
    }
  }
}