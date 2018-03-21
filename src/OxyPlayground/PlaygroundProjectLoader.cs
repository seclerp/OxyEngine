using System;
using System.Diagnostics;
using System.IO;
using OxyEngine.Settings;

namespace OxyPlayground
{
  internal class PlaygroundProjectLoader
  {
    private const string EntryFileName = "entry.py";
    
    internal PlaygroundProject LoadFromArguments(string[] args)
    {
      string projectDirectory = ParseCommands(args);
      string scriptToExecute = Path.Combine(projectDirectory, EntryFileName);
      
      if (!File.Exists(scriptToExecute))
        throw new FileNotFoundException(scriptToExecute);
      
      var project = new PlaygroundProject();
      project.RootFolderPath = Path.IsPathRooted(projectDirectory) 
        ? projectDirectory 
        : Path.Combine(Environment.CurrentDirectory, projectDirectory);
      
      project.Settings = LoadSettings(project.RootFolderPath);   

      project.ScriptsFolderPath = Path.Combine(project.RootFolderPath, project.Settings.ScriptsFolder);
      project.EntryScriptPath = Path.Combine(project.ScriptsFolderPath, EntryFileName);
      project.EntryScriptName = EntryFileName;
      project.ContentFolderPath = Path.Combine(project.RootFolderPath, project.Settings.ContentFolder);

      return project;
    }
    
    /// <summary>
    /// Return entry directory
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal string ParseCommands(string[] args)
    {
      var entryDirectory = Environment.CurrentDirectory;
      var entryDirFound = false;
      
      for (int i = 0; i < args.Length; i++)
      {
        var arg = args[i];
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
    
    private SettingsRoot LoadSettings(string projectRoot)
    {
      var settingsManager = new SettingsManager();
      try
      {
        var loadedSettings = settingsManager.LoadSettings(Path.Combine(projectRoot, "settings.xml"));
        return loadedSettings;
      }
      catch (Exception e)
      {
        Debug.Print(e.ToString());
        return new SettingsRoot();
      }
    }
  }
}