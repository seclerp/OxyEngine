using System.Collections.Generic;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Oxy.Framework
{
  public class GraphicsState
  {
    public Color ForegroundColor { get; set; }
    public Color BackgroundColor { get; set; }
    public float LineWidth { get; set; }
    public SpriteFont Font { get; set; }
    
    internal Stack<Transformation> TransformationStack { get; set; }
  }
}