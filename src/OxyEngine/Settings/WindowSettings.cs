using OxyEngine.Interfaces;

namespace OxyEngine.Settings
{
  public class WindowSettings : ISettings
  {
    public string Title { get; set; } = "OxyFramework Game";
    public bool Resizable { get; set; } = false;
    public int Width { get; set; } = 800;
    public int Height { get; set; } = 600;
    public bool IsFullscreen { get; set; } = false;
    public bool AllowBorders { get; set; } = true;
    public bool CursorVisible { get; set; } = true;
    
    public void Apply(GameInstance instance)
    {
      instance.Window.Title = Title;
      instance.Window.AllowUserResizing = Resizable;
      instance.Window.AllowAltF4 = true;
      instance.Window.IsBorderless = !AllowBorders;
      instance.IsMouseVisible = CursorVisible;

      instance.GraphicsDeviceManager.PreferredBackBufferWidth = Width;
      instance.GraphicsDeviceManager.PreferredBackBufferHeight = Height;
      instance.GraphicsDeviceManager.IsFullScreen = IsFullscreen;

      instance.GraphicsDeviceManager.ApplyChanges();
    }
  }
}