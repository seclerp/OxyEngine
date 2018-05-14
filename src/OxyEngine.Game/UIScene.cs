using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;
using OxyEngine.Resources;
using OxyEngine.UI;
using OxyEngine.UI.Models;
using OxyEngine.Window;

namespace OxyEngine.Game
{
  public class UIScene : TransformEntity, IInitializable, IDrawable
  {
    private Canvas _canvas;
    private WindowManager _windowManager;
    private ResourceManager _resourceManager;

    private TextModel _textModel;
    
    public void Init()
    {
      _windowManager = Container.Instance.ResolveByName<WindowManager>(InstanceName.WindowManager);
      _resourceManager = Container.Instance.ResolveByName<ResourceManager>(InstanceName.ResourceManager);

      _textModel = new TextModel
      {
        
      };
      
      _canvas = new Canvas(_windowManager.GetWidth(), _windowManager.GetHeight());
    }
    
    public void Draw()
    {
      _canvas.Draw(() =>
      {
        _canvas.Graphics.Text();
      });
    }
  }
}