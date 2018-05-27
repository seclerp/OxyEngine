using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.GUI.Enums;
using OxyEngine.GUI.Models;
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
        case HorizontalAlignment.Stretch:
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

    public void RenderSliced(Texture2D texture, Rectangle rect, Rectangle sourceRect, Offset offset, Style style = null)
    {
      var source1 = new Rectangle(sourceRect.X, sourceRect.Y, 
        offset.Left, offset.Top);
      var source2 = new Rectangle(sourceRect.X + offset.Left, sourceRect.Y, 
        sourceRect.Size.X - offset.Left - offset.Right, offset.Top);
      var source3 = new Rectangle(sourceRect.X + sourceRect.Size.X - offset.Right, sourceRect.Y, 
        offset.Right, offset.Top);
      
      var source4 = new Rectangle(sourceRect.X, sourceRect.Y + offset.Top, 
        offset.Left, sourceRect.Size.Y - offset.Top - offset.Bottom);
      var source5 = new Rectangle(sourceRect.X + offset.Left, sourceRect.Y + offset.Top, 
        sourceRect.Size.X - offset.Left - offset.Right, sourceRect.Size.Y - offset.Top - offset.Bottom);
      var source6 = new Rectangle(sourceRect.X + sourceRect.Size.X - offset.Right, sourceRect.Y + offset.Top, 
        offset.Right, sourceRect.Size.Y - offset.Top - offset.Bottom);
      
      var source7 = new Rectangle(sourceRect.X, sourceRect.Y + sourceRect.Size.Y - offset.Bottom, 
        offset.Left, offset.Bottom);
      var source8 = new Rectangle(sourceRect.X + offset.Left, sourceRect.Y + sourceRect.Size.Y - offset.Bottom, 
        sourceRect.Size.X - offset.Left - offset.Right, offset.Bottom);
      var source9 = new Rectangle(sourceRect.X + sourceRect.Size.X - offset.Right, sourceRect.Y + sourceRect.Size.Y - offset.Bottom, 
        offset.Right, offset.Bottom);
      
      var dest1 = new Rectangle(rect.X, rect.Y, 
        offset.Left, offset.Top);
      var dest2 = new Rectangle(rect.X + offset.Left, rect.Y, 
        rect.Size.X - offset.Left - offset.Right, offset.Top);
      var dest3 = new Rectangle(rect.X + rect.Size.X - offset.Right, rect.Y, 
        offset.Right, offset.Top);
      
      var dest4 = new Rectangle(rect.X, rect.Y + offset.Top, 
        offset.Left, rect.Size.Y - offset.Top - offset.Bottom);
      var dest5 = new Rectangle(rect.X + offset.Left, rect.Y + offset.Top, 
        rect.Size.X - offset.Left - offset.Right, rect.Size.Y - offset.Top - offset.Bottom);
      var dest6 = new Rectangle(rect.X + rect.Size.X - offset.Right, rect.Y + offset.Top, 
        offset.Right, rect.Size.Y - offset.Top - offset.Bottom);
      
      var dest7 = new Rectangle(rect.X, rect.Y + rect.Size.Y - offset.Bottom, 
        offset.Left, offset.Bottom);
      var dest8 = new Rectangle(rect.X + offset.Left, rect.Y + rect.Size.Y - offset.Bottom, 
        rect.Size.X - offset.Left - offset.Right, offset.Bottom);
      var dest9 = new Rectangle(rect.X + rect.Size.X - offset.Right, rect.Y + rect.Size.Y - offset.Bottom, 
        offset.Right, offset.Bottom);
      
      // TODO: Remove Render calls, use direct GraphicsManager.DrawCropped instead
      
      AreaStack.Push(dest1);
      Render(texture, dest1, source1, style);
      AreaStack.Pop();
      
      AreaStack.Push(dest2);
      Render(texture, dest2, source2, style);
      AreaStack.Pop();

      AreaStack.Push(dest3);
      Render(texture, dest3, source3, style);
      AreaStack.Pop();

      AreaStack.Push(dest4);
      Render(texture, dest4, source4, style);
      AreaStack.Pop();

      AreaStack.Push(dest5);
      Render(texture, dest5, source5, style);
      AreaStack.Pop();

      AreaStack.Push(dest6);
      Render(texture, dest6, source6, style);
      AreaStack.Pop();

      AreaStack.Push(dest7);
      Render(texture, dest7, source7, style);
      AreaStack.Pop();

      AreaStack.Push(dest8);
      Render(texture, dest8, source8, style);
      AreaStack.Pop();

      AreaStack.Push(dest9);
      Render(texture, dest9, source9, style);
      AreaStack.Pop();
    }
  }
}