using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OxyEngine.Dependency;
using OxyEngine.Graphics;

namespace OxyEngine.UI.Renderers
{
  public class Renderer
  {
    protected readonly AreaStack AreaStack;
    protected GraphicsManager GraphicsManager;

    public Renderer(AreaStack areaStack)
    {
      AreaStack = areaStack;
      GraphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }
  }
}