using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.GUI.Enums;
using OxyEngine.GUI.Styles;

namespace OxyEngine.GUI.Renderers
{
  public class ImageRenderer : Renderer
  {
    public ImageRenderer(AreaStack areaStack, StyleDatabase styles) : base(areaStack, styles)
    {
    }

    public void Render(Texture2D texture, Rectangle rect, Rectangle sourceRect, Style style = null)
    {
      style = style ?? GetDefaultStyle();
      
      var backColorValue = style.GetRule<Color>("background-color");
      var beforeColor = GraphicsManager.GetColor();
      GraphicsManager.SetColor(backColorValue.R, backColorValue.G, backColorValue.B, backColorValue.A);
      GraphicsManager.Rectangle("fill", rect.X, rect.Y, rect.Width, rect.Height);

      var finalX = 0;
      var finalY = 0;

      var finalWidth = sourceRect.Size.X;
      var finalHeight = sourceRect.Size.Y;
    
      switch (style.GetRule<ImageSizeMode>("image-size-mode"))
      {
        case ImageSizeMode.KeepSize:
          break;
          
        case ImageSizeMode.Stretch:
          finalWidth = rect.Size.X;
          finalHeight = rect.Size.Y;
          
          break;
          
        case ImageSizeMode.Contain:
          // 1. Find smallest side of container
          var smallestContainerSide = Math.Min(rect.Size.X, rect.Size.Y);
          
          // 2. Get ratio between smallest side of container and child
          float containRatio = smallestContainerSide == rect.Size.X 
            ? (float)smallestContainerSide / sourceRect.Size.X 
            : (float)smallestContainerSide / sourceRect.Size.Y;
          
          // 3. Multiply sides by this ratio
          finalWidth = (int)(sourceRect.Size.X * containRatio);
          finalHeight = (int)(sourceRect.Size.Y * containRatio);
          
          break;
        
        case ImageSizeMode.Cover:
          // 1. Find biggest side of container
          var smallestSourceSide = Math.Min(sourceRect.Size.X, sourceRect.Size.Y);
          
          // 2. Get ratio between smallest side of container and child
          float coverRatio = smallestSourceSide == rect.Size.X 
            ? (float)smallestSourceSide / sourceRect.Size.X 
            : (float)smallestSourceSide / sourceRect.Size.Y;
          
          // 3. Multiply sides by this ratio
          finalWidth = (int)(sourceRect.Size.X * coverRatio);
          finalHeight = (int)(sourceRect.Size.Y * coverRatio);
          
          break;
        
        default:
          throw new ArgumentOutOfRangeException();
      }
      
      switch (style.GetRule<HorizontalAlignment>("h-align"))
      {
        case HorizontalAlignment.Left:
          finalX = 0;
          break;
        case HorizontalAlignment.Center:
        case HorizontalAlignment.FullWidth:
          finalX = (rect.Size.X - finalWidth) / 2;
          break;
        case HorizontalAlignment.Right:
          finalX = rect.Size.X - finalWidth;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      
      switch (style.GetRule<VerticalAlignment>("v-align"))
      {
        case VerticalAlignment.Top:
          finalY = 0;
          break;
        case VerticalAlignment.Middle:
        case VerticalAlignment.Stretch:
          finalY = (rect.Size.Y - finalHeight) / 2;
          break;
        case VerticalAlignment.Bottom:
          finalY = rect.Size.Y - finalHeight;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      
      var destRect = new Rectangle(rect.X + finalX, rect.Y + finalY, finalWidth, finalHeight);
      
      GraphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
      
      GraphicsManager.DrawCropped(rect, () =>
      {
        GraphicsManager.Draw(texture, sourceRect, destRect);
      });
    }
  }
}