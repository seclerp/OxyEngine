using System.IO;
using System.Xml.Serialization;

namespace OxyEngine.Settings
{
  public class SettingsManager
  {
    public SettingsRoot LoadSettings(string filePath)
    {
      if (!File.Exists(filePath))
        throw new FileNotFoundException(filePath);

      var xmlSerializer = new XmlSerializer(typeof(SettingsRoot));
      using (var file = File.OpenRead(filePath))
      {
        return (SettingsRoot)xmlSerializer.Deserialize(file);
      }
    }
  }
}