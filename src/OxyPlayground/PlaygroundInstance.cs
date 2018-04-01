using OxyEngine;
using OxyEngine.Projects;
using OxyEngine.Python;

namespace OxyPlayground
{
  public class PlaygroundInstance : GameInstance
  {
    public PlaygroundInstance(GameProject project) : base(project)
    {
      SetScripting(new PythonScriptingManager());
    }

    protected override void LoadContent()
    {
      var api = GetApi();
      api.Scripting.ExecuteScript(Project.EntryScriptName);
      
      base.LoadContent();
    }
  }
}