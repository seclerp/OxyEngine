namespace OxyEngine.Interfaces
{
  public interface IScripting
  {
    void Initialize(string scriptsRootFolder);
    void AddNetModule(string reference);
    void RemoveNetModule(string reference);
    void ExecuteScript(string path);
  }
}