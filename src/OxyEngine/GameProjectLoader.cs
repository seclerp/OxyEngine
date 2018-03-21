using System;
using System.Diagnostics;
using System.IO;
using OxyEngine.Settings;

namespace OxyEngine
{
  public class GameProjectLoader
  {
    private const string EntryFileName = "entry.py";
    private const string SettingsFileName = "settings.xml";
    
    /// <summary>
    ///   Loads project using given command-line arguments
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public GameProject LoadFromArguments(string[] args)
    {
      string projectDirectory = ParseCommands(args);
      string scriptToExecute = Path.Combine(projectDirectory, EntryFileName);
      
      if (!File.Exists(scriptToExecute))
        throw new FileNotFoundException(scriptToExecute);

      return LoadFromFolder(projectDirectory);
    }

    public GameProject LoadFromFolder(string projectDirectory)
    {
      var fullProjectPath =  Path.IsPathRooted(projectDirectory) 
        ? projectDirectory 
        : Path.Combine(Environment.CurrentDirectory, projectDirectory);
      
      if (!Directory.Exists(fullProjectPath))
        throw new DirectoryNotFoundException(fullProjectPath);

      var project = new GameProject { RootFolderPath = fullProjectPath };

      project.GameSettings = LoadSettings(project.RootFolderPath);   
      project.ScriptsFolderPath = Path.Combine(project.RootFolderPath, project.GameSettings.ScriptsFolder);
      project.EntryScriptPath = Path.Combine(project.ScriptsFolderPath, EntryFileName);
      project.EntryScriptName = EntryFileName;
      project.ContentFolderPath = Path.Combine(project.RootFolderPath, project.GameSettings.ContentFolder);

      return project;
    }
    
    /// <summary>
    /// Return entry directory
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private string ParseCommands(string[] args)
    {
      var entryDirectory = Environment.CurrentDirectory;
      var entryDirFound = false;
      
      foreach (var arg in args)
      {
        switch (arg)
        {
          case "-d":
          case "--debug":
//            Window.SetDebugMode();
            break;
          default:
            if (entryDirFound)
              throw new Exception($"Unknown command line argument: '{arg}'");
            
            // Suppose that this agument is a path to folder or entry file
            entryDirFound = true;
            entryDirectory = ParseEntryDirectory(arg);
            break;
        }
      }

      return entryDirectory;
    }

    private string ParseEntryDirectory(string arg)
    {
      if (File.Exists(arg))
      {
        var directoryInfo = new FileInfo(arg).Directory;
        if (directoryInfo != null)
          return directoryInfo.FullName;
        
        throw new Exception($"Error finding project path for entry script '{arg}'.");
      }
      
      if (Directory.Exists(arg))
        return arg;
      
      throw new Exception($"File or directory not found: '{arg}'");
    }
    
    private GameSettingsRoot LoadSettings(string projectRoot)
    {
      var settingsManager = new SettingsManager();
      try
      {
        var loadedSettings = settingsManager.LoadSettings(Path.Combine(projectRoot, SettingsFileName));
        return loadedSettings;
      }
      catch (Exception e)
      {
        Debug.Print(e.ToString());
        return new GameSettingsRoot();
      }
    }
  }
}