using System;
using Microsoft.Xna.Framework;
using OxyEngine.UI.Enums;

namespace OxyEngine.UI.Renderers
{
  public class RootRenderer : Renderer
  {
    private LayoutGraphicsRenderer _horizontalLayoutRenderer;
    private LayoutGraphicsRenderer _verticalLayoutRenderer;
    private FreeGraphicsRenderer _freeGraphicsRenderer;
    
    public RootRenderer(AreaStack areaStack) : base(areaStack)
    {
      _horizontalLayoutRenderer = new LayoutGraphicsRenderer(areaStack);
      _verticalLayoutRenderer = new LayoutGraphicsRenderer(areaStack);
      _freeGraphicsRenderer = new FreeGraphicsRenderer(areaStack);
    }
    
    public void HorizontalLayout(Action<LayoutGraphicsRenderer> action)
    {
      
    }
    
    public void VerticalLayout(Action<LayoutGraphicsRenderer> action)
    {
      
    }
    
    public void FreeLayout(Rectangle rectangle, Action<FreeGraphicsRenderer> action)
    {
      AreaStack.Push(rectangle);
      GraphicsManager.DrawCropped(AreaStack.Peek(), () => action(_freeGraphicsRenderer));
      AreaStack.Pop();
    }
  }
}