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
      public static class Update
      {
        public const string OnBeginUpdate = "beginupdate";
        public const string OnUpdate = "update";
        public const string OnEndUpdate = "endupdate";
      }
      
      public static class Draw
      {
        public const string OnBeginDraw = "begindraw";
        public const string OnDraw = "draw";
        public const string OnEndDraw = "enddraw";
      }
    }
    
    public static class Window
    {
      public const string OnResize = "resize";
    }
  }
}