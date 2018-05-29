namespace OxyEngine.GUI.States
{
  public struct ButtonState
  {
    public readonly bool IsPressed;
    public readonly bool IsClicked;
    public readonly bool IsMouseOver;

    public ButtonState(bool isPressed, bool isClicked, bool isMouseOver)
    {
      IsPressed = isPressed;
      IsClicked = isClicked;
      IsMouseOver = isMouseOver;
    }
  }
}