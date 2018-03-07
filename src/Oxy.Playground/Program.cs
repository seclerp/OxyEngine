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
      string projectDirectory = Environment.CurrentDirectory;
      
      if (args.Length > 0)
      {
        if (File.Exists(args[0]))
        {
          var directoryInfo = new FileInfo(args[0]).Directory;
          if (directoryInfo != null)
            projectDirectory = directoryInfo.FullName;
          else
            throw new Exception($"Error finding project path for entry script '{args[0]}'.");
        }
        else if (Directory.Exists(projectDirectory))
          projectDirectory = args[0];
        else
          throw new Exception($"File or directory not found: '{args[0]}'");
      }

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
  }
}