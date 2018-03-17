using Oxy.Framework.Settings;

namespace Oxy.Playground
{
  public class PlaygroundProject
  {
    public SettingsRoot Settings { get; set; }
    public string RootFolderPath { get; set; }
    public string EntryScriptPath { get; set; }
    public string ContentFolderPath { get; set; }
    public string ScriptsFolderPath { get; set; }
  }
}