namespace OxyEngine.Interfaces
{
  public interface IScripting
  {
    bool GenerateScriptStacktrace { get; set; }
    
    void Initialize(string scriptsRootFolder);
    void AddNetModule(string reference);
    void RemoveNetModule(string reference);
    void ExecuteScript(string path);
    void SetGlobal(string name, object value);
    void GetGlobal<T>(string name);
  }
}