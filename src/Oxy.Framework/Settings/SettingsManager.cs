using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using Oxy.Framework.Settings;

namespace Oxy.Framework.Settings
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