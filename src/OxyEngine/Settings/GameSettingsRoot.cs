using System.Xml.Serialization;
using OxyEngine.Interfaces;

namespace OxyEngine.Settings
{
  [XmlRoot(ElementName="Settings")]
  public class GameSettingsRoot : ISettings
  {
    public string ContentFolder { get; set; } = "";
    public string ScriptsFolder { get; set; } = "";

    [XmlElement(ElementName = "Window")]
    public WindowSettings WindowSettings { get; set; } = new WindowSettings();
    [XmlElement(ElementName = "Graphics")]
    public GraphicsSettings GraphicsSettings { get; set; } = new GraphicsSettings();
    [XmlElement(ElementName = "Resources")]
    public ResourcesSettings ResourcesSettings { get; set; } = new ResourcesSettings();

    public void Apply(GameInstance instance)
    {
      WindowSettings.Apply(instance);
      GraphicsSettings.Apply(instance);
      ResourcesSettings.Apply(instance);
    }
  }
}