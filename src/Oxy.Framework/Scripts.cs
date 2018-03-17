using System;
using System.Collections.Generic;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Oxy.Framework
{
  public class Scripts : IModule
  {
    // Those modules cannot be switched off and need to be for correct work of other modules
    private static readonly List<string> ImportantScriptAssemblies = new List<string>
    {
      "MonoGame.Framework",
      "Oxy.Framework",
    };
    
    private static readonly List<string> ScriptAssemblies = new List<string>();

    private readonly ScriptEngine _scriptEngine;
    private static string _scriptsRootFolder;

    public Scripts()
    {
      _scriptEngine = Python.CreateEngine();
    }

    private ScriptScope CreateConfiguredScope()
    {
      ScriptScope scope = _scriptEngine.CreateScope();
      scope.ImportModule("clr");

      _scriptEngine.Execute("import clr", scope);

      foreach (var module in ImportantScriptAssemblies)
        _scriptEngine.Execute($"clr.AddReference(\"{module}\")", scope);

      foreach (var module in ScriptAssemblies)
        _scriptEngine.Execute($"clr.AddReference(\"{module}\")", scope);

      return scope;
    }

    /// <summary>
    ///   Add .NET assembly reference to the list of imported libs for executing scripts
    /// </summary>
    /// <param name="reference">Reference name or full name</param>
    public void AddDefaultNetModule(string reference)
    {
      if (!ScriptAssemblies.Contains(reference))
        ScriptAssemblies.Add(reference);
    }

    /// <summary>
    ///   Remove .NET assembly reference from the list of imported libs for executing scripts
    /// </summary>
    /// <param name="reference">Reference name or full name</param>
    public void RemoveDefaultNetModule(string reference)
    {
      if (ScriptAssemblies.Contains(reference))
        ScriptAssemblies.Remove(reference);
    }

    /// <summary>
    ///   Set scripts root folder
    /// </summary>
    /// <param name="path">Path to the scripts root folder</param>
    /// <exception cref="DirectoryNotFoundException">If library directory not exists</exception>
    public void SetScriptsRoot(string path)
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

    /// <summary>
    ///   Executes Python scripts with importing .NET modules
    /// </summary>
    /// <param name="path">Path to the script in scripts folder</param>
    public void ExecuteScript(string path)
    {
      ScriptScope scope = CreateConfiguredScope();

      try
      {
        _scriptEngine.ExecuteFile(Path.Combine(_scriptsRootFolder, path), scope);
      }
      catch (Exception e)
      {
        var stackTrace = _scriptEngine.GetService<ExceptionOperations>().FormatException(e);
        throw new Exception(stackTrace, e);
      }
    }
  }
}