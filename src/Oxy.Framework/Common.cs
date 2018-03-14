using System;
using System.Collections.Generic;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Oxy.Framework.Exceptions;

namespace Oxy.Framework
{
  public class Common : Module<Common>
  {
    // Those modules cannot be switched off and need to be for correct work of other modules
    private readonly List<string> _mustHaveScriptModules = new List<string>
    {
      "framework"
    };

    internal static readonly ScriptEngine PythonScriptEngine;

    private readonly List<string> _scriptModules = new List<string>
    {
      
    };

    private string _assetsRootFolder;

    private string _scriptsRootFolder;

    internal static bool ScriptExecuting = false;

    static Common()
    {
      PythonScriptEngine = Python.CreateEngine();
    }

    private ScriptScope CreateConfiguredScope()
    {
      ScriptScope scope = PythonScriptEngine.CreateScope();
      scope.ImportModule("clr");

      PythonScriptEngine.Execute("import clr", scope);

      foreach (var module in _mustHaveScriptModules)
        PythonScriptEngine.Execute($"clr.AddReference(\"{module}\")", scope);

      foreach (var module in _scriptModules)
        PythonScriptEngine.Execute($"clr.AddReference(\"{module}\")", scope);

      return scope;
    }

    /// <summary>
    ///   Add .NET assembly reference to the list of imported libs for executing scripts
    /// </summary>
    /// <param name="reference">Reference name or full name</param>
    public static void AddDefaultNetModule(string reference)
    {
      if (!Instance._scriptModules.Contains(reference))
        Instance._scriptModules.Add(reference);
    }

    /// <summary>
    ///   Remove .NET assembly reference from the list of imported libs for executing scripts
    /// </summary>
    /// <param name="reference">Reference name or full name</param>
    public static void RemoveDefaultNetModule(string reference)
    {
      if (Instance._scriptModules.Contains(reference))
        Instance._scriptModules.Remove(reference);
    }

    /// <summary>
    ///   Set scripts root folder
    /// </summary>
    /// <param name="path">Path to the scripts root folder</param>
    /// <exception cref="DirectoryNotFoundException">If library directory not exists</exception>
    public static void SetScriptsRoot(string path)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException(path);

      var paths = PythonScriptEngine.GetSearchPaths();

      if (!string.IsNullOrEmpty(Instance._scriptsRootFolder))
        paths.Remove(Instance._scriptsRootFolder);
      
      Instance._scriptsRootFolder = path;
      paths.Add(Instance._scriptsRootFolder);
      PythonScriptEngine.SetSearchPaths(paths);
    }

    /// <summary>
    ///   Set library root folder
    /// </summary>
    /// <param name="path">Path to the library root folder</param>
    /// <exception cref="DirectoryNotFoundException">If library directory not exists</exception>
    public static void SetAssetsRoot(string path)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException(path);

      Instance._assetsRootFolder = path;
    }

    /// <summary>
    ///   Returns scripts root folder
    /// </summary>
    /// <returns>Scripts root folder</returns>
    public static string GetScriptsRoot()
    {
      return Instance._scriptsRootFolder;
    }

    /// <summary>
    ///   Returns library root path
    /// </summary>
    /// <returns>Library root path</returns>
    public static string GetAssetsRoot()
    {
      return Instance._assetsRootFolder;
    }

    /// <summary>
    ///   Executes Python scripts with importing .NET modules
    /// </summary>
    /// <param name="path">Path to the script in scripts folder</param>
    public static void ExecuteScript(string path)
    {
      ScriptScope scope = Instance.CreateConfiguredScope();

      try
      {
        ScriptExecuting = true;
        PythonScriptEngine.ExecuteFile(Path.Combine(Instance._scriptsRootFolder, path), scope);
      }
      catch (Exception e)
      {
        var stackTrace = PythonScriptEngine.GetService<ExceptionOperations>().FormatException(e);

        Window.Error(new PyException(e.Message, stackTrace));
      }
      finally
      {
        ScriptExecuting = false;
      }
    }
  }
}