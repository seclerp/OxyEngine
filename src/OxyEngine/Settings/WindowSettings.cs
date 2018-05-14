using Microsoft.Xna.Framework;
using OxyEngine.Dependency;
using OxyEngine.Interfaces;
using OxyEngine.Window;

namespace OxyEngine.Settings
{
  public class WindowSettings : ISettings
  {
    public string Title { get; set; } = "OxyEngine Game";
    public bool Resizable { get; set; } = false;
    public int Width { get; set; } = 800;
    public int Height { get; set; } = 600;
    public bool IsFullscreen { get; set; } = false;
    public bool AllowBorders { get; set; } = true;
    public bool CursorVisible { get; set; } = true;
    
    public void Apply(GameInstance instance)
    {
      (instance as Game).Window.AllowAltF4 = true;

      var window = Container.Instance.ResolveByName<WindowManager>(InstanceName.WindowManager);
      
      window.SetTitle(Title);
      window.SetResizable(Resizable);
      window.SetBorders(AllowBorders);
      window.SetCursorVisible(CursorVisible);

      window.SetWidth(Width);
      window.SetHeight(Height);
      window.SetFullscreen(IsFullscreen);
      
      window.ApplyChanges();
    }
  }
}