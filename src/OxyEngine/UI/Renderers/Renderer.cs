using OxyEngine.Dependency;
using OxyEngine.Graphics;

namespace OxyEngine.UI.Renderers
{
  public class Renderer
  {
    protected GraphicsManager GraphicsManager;

    public Renderer()
    {
      GraphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }
  }
}