using System.IO;
using System.Xml.Serialization;

namespace OxyEngine.Settings
{
  public class SettingsManager
  {
    public GameSettingsRoot LoadSettings(string filePath)
    {
      if (!File.Exists(filePath))
        throw new FileNotFoundException(filePath);

      var xmlSerializer = new XmlSerializer(typeof(GameSettingsRoot));
      using (var file = File.OpenRead(filePath))
      {
        return (GameSettingsRoot)xmlSerializer.Deserialize(file);
      }
    }
  }
}