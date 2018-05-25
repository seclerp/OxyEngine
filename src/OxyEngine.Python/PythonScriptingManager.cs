using System;
using System.Collections.Generic;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using OxyEngine.Interfaces;

namespace OxyEngine.Python
{
  public class PythonScriptingManager : IModule, IScriptingManager
  {
    // Those modules cannot be switched off and need to be for correct work of other modules
    private static readonly List<string> ImportantScriptAssemblies = new List<string>
    {
      "MonoGame.Framework",
      "OxyEngine"
    };
    
    private static readonly List<string> ScriptAssemblies = new List<string>();

    private ScriptEngine _scriptEngine;
    private static string _scriptsRootFolder;

    public bool GenerateScriptStacktrace { get; set; }

    public void Initialize(string scriptsRootFolder)
    {
      _scriptEngine = IronPython.Hosting.Python.CreateEngine();
      SetScriptsRoot(scriptsRootFolder);
    }

    /// <summary>
    ///   Add .NET assembly reference to the list of imported libs for executing scripts
    /// </summary>
    /// <param name="reference">Reference name or full name</param>
    public void AddNetModule(string reference)
    {
      if (!ScriptAssemblies.Contains(reference))
        ScriptAssemblies.Add(reference);
    }

    /// <summary>
    ///   Remove .NET assembly reference from the list of imported libs for executing scripts
    /// </summary>
    /// <param name="reference">Reference name or full name</param>
    public void RemoveNetModule(string reference)
    {
      if (ScriptAssemblies.Contains(reference))
        ScriptAssemblies.Remove(reference);
    }

    private void SetScriptsRoot(string path)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException(path);

      var paths = _scriptEngine.GetSearchPaths();

      if (!string.IsNullOrEmpty(_scriptsRootFolder))
        paths.Remove(_scriptsRootFolder);
      
      _scriptsRootFolder = path;
      paths.Add(_scriptsRootFolder);
      _scriptEngine.SetSearchPaths(paths);
    }

    public void ExecuteScript(string path)
    {
      var scope = CreateConfiguredScope();

      try
      {
        _scriptEngine.ExecuteFile(Path.Combine(_scriptsRootFolder, path), scope);
      }
      catch (Exception e)
      {
        if (!GenerateScriptStacktrace)
          return;
        
        var stackTrace = _scriptEngine.GetService<ExceptionOperations>().FormatException(e);
        throw new Exception(stackTrace, e);
      }
    }

    public void SetGlobal(string name, object value)
    {
      _scriptEngine.GetBuiltinModule().SetVariable(name, value);
    }

    public void GetGlobal<T>(string name)
    {
      _scriptEngine.GetBuiltinModule().GetVariable<T>(name);
    }

    private ScriptScope CreateConfiguredScope()
    {
      var scope = _scriptEngine.CreateScope();
      scope.ImportModule("clr");

      _scriptEngine.Execute("import clr", scope);

      foreach (var module in ImportantScriptAssemblies)
        _scriptEngine.Execute($"clr.AddReference(\"{module}\")", scope);

      foreach (var module in ScriptAssemblies)
        _scriptEngine.Execute($"clr.AddReference(\"{module}\")", scope);

      return scope;
    }
  }
}