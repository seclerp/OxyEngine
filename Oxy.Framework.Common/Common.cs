using System;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Oxy.Framework
{
  public class Common : Module<Common>
  {
    private ScriptEngine _scriptEngine;
    private string _scriptsRootFolder;

    public Common()
    {
      _scriptEngine = Python.CreateEngine();
    }

    private ScriptScope CreateConfiguredScope()
    {
      ScriptScope scope = _scriptEngine.CreateScope();
      scope.ImportModule("clr");

      _scriptEngine.Execute("import clr", scope);
      _scriptEngine.Execute("clr.AddReference(\"Oxy.Framework.Graphics\")", scope);
      _scriptEngine.Execute("clr.AddReference(\"Oxy.Framework.Input\")", scope);
      _scriptEngine.Execute("clr.AddReference(\"Oxy.Framework.Window\")", scope);

      return scope;
    }

    public static void SetScriptsRoot(string path)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException(path);

      Instance._scriptsRootFolder = path;
    }

    public static void ExecuteScript(string path)
    {
      try 
      {
        ScriptScope scope = Instance.CreateConfiguredScope();
        Instance._scriptEngine.ExecuteFile(Path.Combine(Instance._scriptsRootFolder, path), scope);
      }
      catch(Exception e)
      {
        Console.WriteLine($"Python error ({path}): {e.Message}");
      }
    }
  }
}