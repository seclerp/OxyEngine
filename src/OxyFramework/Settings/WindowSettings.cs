namespace OxyFramework.Settings
{
  public class WindowSettings
  {
    public string Title { get; set; } = "OxyFramework Game";
    public bool Resizable { get; set; } = false;
    public int Width { get; set; } = 800;
    public int Height { get; set; } = 600;
    public bool IsFullscreen { get; set; } = false;
    public bool AllowBorders { get; set; } = true;
  }
}