namespace OxyEngine.Interfaces
{
  public interface IScripting
  {
    void Initialize(string projectRootFolder);
    void AddNetModule(string reference);
    void RemoveNetModule(string reference);
    void ExecuteScript(string path);
  }
}