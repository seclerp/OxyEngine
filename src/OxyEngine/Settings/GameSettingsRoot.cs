using System.Xml.Serialization;

namespace OxyEngine.Settings
{
  [XmlRoot(ElementName="Settings")]
  public class GameSettingsRoot
  {
    public string ContentFolder { get; set; } = "Content";
    public string ScriptsFolder { get; set; } = "";
    
    [XmlElement(ElementName = "Window")]
    public WindowSettings WindowSettings { get; set; }
    [XmlElement(ElementName = "Graphics")]
    public GraphicsSettings GraphicsSettings { get; set; }
    [XmlElement(ElementName = "Resources")]
    public ResourcesSettings ResourcesSettings { get; set; }
  }
}