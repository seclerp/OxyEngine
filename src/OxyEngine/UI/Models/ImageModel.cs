using Microsoft.Xna.Framework.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.Models
{
  public class ImageModel : GraphicsWidgetModel
  {
    public Texture2D ImageTexture { get; set; }
    
    public ImageFitMode FitMode { get; set; }

    public ImageModel(WidgetNode node) : base(node)
    {
    }
  }
}