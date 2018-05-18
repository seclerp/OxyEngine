using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OxyEngine.Graphics
{
  public class GraphicsState
  {
    public Color ForegroundColor { get; set; }
    public Color BackgroundColor { get; set; }
    public float LineWidth { get; set; }
    public SpriteFont Font { get; set; }
    public Rectangle BackupCropping { get; set; }
    public RasterizerState RasterizerState { get; set; }
    
    internal Stack<Transformation> TransformationStack { get; set; }
  }
}