using OxyEngine.Settings;

namespace OxyEngine.Projects
{
  public class GameProject
  {
    public GameProject()
    {
    }

    public GameProject(GameSettingsRoot gameSettings)
    {
      GameSettings = gameSettings;
    }
    
    public GameSettingsRoot GameSettings { get; set; }
    public string RootFolderPath { get; set; }
    public string EntryScriptPath { get; set; }
    public string EntryScriptName { get; set; }
    public string ContentFolderPath { get; set; }
    public string ScriptsFolderPath { get; set; }
  }
}