using System.Text;
using OxyEngine;
using OxyEngine.Loggers;
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
      // TODO Remove
      api.Events.Global.LogListenerRegistration = true;
      api.Scripting.ExecuteScript(Project.EntryScriptName);
      
      base.LoadContent();
    }
  }
}