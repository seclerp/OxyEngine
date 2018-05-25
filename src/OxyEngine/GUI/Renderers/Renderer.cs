using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.GUI.Styles;

namespace OxyEngine.GUI.Renderers
{
  public class Renderer
  {
    protected readonly AreaStack AreaStack;
    protected GraphicsManager GraphicsManager;
    protected StyleDatabase Styles;

    public Renderer(AreaStack areaStack, StyleDatabase styles)
    {
      AreaStack = areaStack;
      Styles = styles;
      GraphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }

    protected Style GetDefaultStyle()
    {
      return Styles.GetStyle("");
    }
  }
}