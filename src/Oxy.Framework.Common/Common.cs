using System.Collections.Generic;
using System.IO;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Oxy.Framework
{
  public class Common : Module<Common>
  {
    private string _libraryRootFolder;

    // Those modules cannot be switched off and need to be for correct work of other modules
    private readonly List<string> _mustHaveScriptModules = new List<string>
    {
      "Oxy.Framework.Common"
    };

    private readonly ScriptEngine _scriptEngine;

    private readonly List<string> _scriptModules = new List<string>
    {
      "Oxy.Framework.Graphics",
      "Oxy.Framework.Input",
      "Oxy.Framework.Window",
      "Oxy.Framework.Resources"
    };

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

      foreach (var module in _mustHaveScriptModules)
        _scriptEngine.Execute($"clr.AddReference(\"{module}\")", scope);

      foreach (var module in _scriptModules)
        _scriptEngine.Execute($"clr.AddReference(\"{module}\")", scope);

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

      Instance._scriptsRootFolder = path;
    }

    /// <summary>
    ///   Set library root folder
    /// </summary>
    /// <param name="path">Path to the library root folder</param>
    /// <exception cref="DirectoryNotFoundException">If library directory not exists</exception>
    public static void SetLibraryRoot(string path)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException(path);

      Instance._libraryRootFolder = path;
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
    public static string GetLibraryRoot()
    {
      return Instance._libraryRootFolder;
    }

    /// <summary>
    ///   Executes Python scripts with importing .NET modules
    /// </summary>
    /// <param name="path">Path to the script in scripts folder</param>
    public static void ExecuteScript(string path)
    {
      ScriptScope scope = Instance.CreateConfiguredScope();
      Instance._scriptEngine.ExecuteFile(Path.Combine(Instance._scriptsRootFolder, path), scope);
    }
  }
}