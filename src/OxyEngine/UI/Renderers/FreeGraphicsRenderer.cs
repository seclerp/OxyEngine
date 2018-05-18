using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;

namespace OxyEngine.UI.Renderers
{
  public class FreeGraphicsRenderer : RootRenderer
  {
    private AreaWrapper _areaWrapper;
    private GraphicsManager _graphicsManager;
    
    public FreeGraphicsRenderer(AreaWrapper areaWrapper) : base(areaWrapper)
    {
      _areaWrapper = areaWrapper;
      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }
    
    public void Text(Rectangle rect, SpriteFont font, string text, Color textColor, Color backColor, 
      HorizontalAlignment hTextAlign = HorizontalAlignment.Left, VerticalAlignment vTextAlign = VerticalAlignment.Top)
    {
      
    }
    
    public void Text(TextModel model)
    {
      Text(model.Rect, model.Font, model.Text, model.TextColor, model.BackgroundColor, model.HorizontalAlignment, model.VerticalAlignment);
    }
  }
}