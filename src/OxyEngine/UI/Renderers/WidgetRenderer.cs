using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.Renderers
{
  // TODO: Implement
  public abstract class WidgetRenderer : WidgetPart
  {
    protected GraphicsManager GraphicsApi;
    
    protected WidgetRenderer(WidgetNode node)
    {
      Node = node;
      
      GraphicsApi = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }

    public abstract void Render();
  }
}