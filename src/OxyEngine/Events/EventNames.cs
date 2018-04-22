namespace OxyEngine.Events
{
  public static class EventNames
  {
    public static class Initialization
    {
      public const string OnInit = "init";
      public const string OnLoad = "load";
      public const string OnUnload = "unload";
    }
    
    public static class Gameloop
    {
      public const string OnUpdate = "update";
    }
    
    public static class Graphics
    {
      public const string OnDraw = "draw";
    }
    
    public static class Window
    {
      public const string OnResize = "resize";
    }
  }
}