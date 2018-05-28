using Microsoft.Xna.Framework.Graphics;
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
    protected Texture2D EmptyTexture2D;
    
    public Renderer(AreaStack areaStack, StyleDatabase styles)
    {
      AreaStack = areaStack;
      Styles = styles;
      GraphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      EmptyTexture2D = GraphicsManager.NewTexture(1, 1);
    }

    protected Style GetDefaultStyle()
    {
      return Styles.GetStyle("");
    }
  }
}