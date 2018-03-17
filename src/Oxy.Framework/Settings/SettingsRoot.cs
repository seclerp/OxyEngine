namespace Oxy.Framework.Settings
{
  public class SettingsRoot
  {
    public string ContentFolder { get; set; } = "Content";
    public string ScriptsFolder { get; set; } = "";
    
    public WindowSettings WindowSettings { get; set; }
    public GraphicsSettings GraphicsSettings { get; set; }
    public ResourcesSettings ResourcesSettings { get; set; }
  }
}