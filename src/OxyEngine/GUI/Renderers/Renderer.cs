using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.GUI.Styles;
using OxyEngine.Input;

namespace OxyEngine.GUI.Renderers
{
  public class Renderer
  {
    protected readonly AreaStack AreaStack;
    protected GraphicsManager GraphicsManager;
    protected InputManager InputManager;
    protected StyleDatabase Styles;
    protected Texture2D EmptyTexture2D;
    
    public Renderer(AreaStack areaStack, StyleDatabase styles)
    {
      AreaStack = areaStack;
      Styles = styles;
      GraphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      InputManager = Container.Instance.ResolveByName<InputManager>(InstanceName.InputManager);
      EmptyTexture2D = GraphicsManager.NewTexture(1, 1);
    }
  }
}