using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Widgets;

namespace OxyEngine.UI.Renderers
{
  // TODO: Implement
  public abstract class WidgetRenderer
  {
    protected GraphicsManager GraphicsApi;
    
    protected WidgetRenderer()
    {
      GraphicsApi = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }

    public abstract void Render();
  }
}