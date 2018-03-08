using System;
using System.IO;
using System.Net;
using Oxy.Framework;

namespace Oxy.Playground
{
  internal class Program
  {
    private const string EntryFileName = "entry.py";
    
    public static void Main(string[] args)
    {
      string projectDirectory = ParseCommands(args);
      
      string scriptToExecute = Path.Combine(projectDirectory, EntryFileName);
      
      if (!File.Exists(scriptToExecute))
        throw new Exception($"Entry script '{scriptToExecute}' not exists.");
      
      var entryScript = new FileInfo(scriptToExecute);

      if (entryScript.Extension != ".py")
        throw new Exception("Entry file must have .py extension.");

      Common.SetLibraryRoot(projectDirectory);
      Common.SetScriptsRoot(projectDirectory);
      
      Common.ExecuteScript(entryScript.FullName);
    }

    
    /// <summary>
    /// Return entry directory
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static string ParseCommands(string[] args)
    {
      var entryDirectory = Environment.CurrentDirectory;
      for (int i = 0; i < args.Length; i++)
      {
        var arg = args[i];
        switch (arg)
        {
          case "-d":
          case "--debug":
            Window.SetDebugMode();
            break;
          default:
            if (i != 0)
              throw new Exception($"Unknown command line argument: '{arg}'");
            entryDirectory = ParseEntryDirectory(arg);
            break;
        }
      }

      return entryDirectory;
    }

    private static string ParseEntryDirectory(string arg)
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
  }
}