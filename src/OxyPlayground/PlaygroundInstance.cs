using OxyEngine;

namespace OxyPlayground
{
  public class PlaygroundInstance : GameInstance
  {
    public PlaygroundInstance(GameProject project) : base(project)
    {
      SetScripting(new PythonScripting());
    }

    protected override void LoadContent()
    {
      var api = GetApi();
      api.Scripting.ExecuteScript(Project.EntryScriptName);
      
      base.LoadContent();
    }
  }
}