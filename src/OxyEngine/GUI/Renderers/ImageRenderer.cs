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

      if (style.GetRule<ImageSizeMode>("image-size-mode") == ImageSizeMode.Sliced)
      {
        RenderSlicedInternal(texture, rect, sourceRect, style);
      }
      else
      {
        RenderInternal(texture, rect, sourceRect, style);
      }
    }

    private void RenderSlicedInternal(Texture2D texture, Rectangle rect, Rectangle sourceRect, Style style)
    {
      var offset = style.GetRule<Offset>("offset");
      
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
      
      RenderImage(texture, dest1, source1);
      RenderImage(texture, dest2, source2);
      RenderImage(texture, dest3, source3);
      RenderImage(texture, dest4, source4);
      RenderImage(texture, dest5, source5);
      RenderImage(texture, dest6, source6);
      RenderImage(texture, dest7, source7);
      RenderImage(texture, dest8, source8);
      RenderImage(texture, dest9, source9);
    }

    private void RenderInternal(Texture2D texture, Rectangle rect, Rectangle sourceRect, Style style)
    {
      var backColorValue = style.GetRule<Color>("background-color");
      var beforeColor = GraphicsManager.GetColor();
      GraphicsManager.SetColor(backColorValue.R, backColorValue.G, backColorValue.B, backColorValue.A);
      GraphicsManager.Rectangle("fill", rect.X, rect.Y, rect.Width, rect.Height);

      var finalSize = CalculateSize(rect, sourceRect, style.GetRule<ImageSizeMode>("image-size-mode"));
      var finalPosition = CalculatePosition(rect, finalSize,
        style.GetRule<HorizontalAlignment>("h-align"), style.GetRule<VerticalAlignment>("v-align"));
      
      var destRect = new Rectangle(rect.X + finalPosition.X, rect.Y + finalPosition.Y, finalSize.X, finalSize.Y);
      
      GraphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);

      RenderImage(texture, destRect, sourceRect);
    }

    private Point CalculateSize(Rectangle rect, Rectangle sourceRect, ImageSizeMode sizeMode)
    {
      switch (sizeMode)
      {
        case ImageSizeMode.KeepSize:
          return new Point(sourceRect.Size.X, sourceRect.Size.Y);

        case ImageSizeMode.Stretch:
          return new Point(rect.Size.X, rect.Size.Y);
          
        case ImageSizeMode.Contain:
          var smallestContainerSide = Math.Min(rect.Size.X, rect.Size.Y);
          
          var containRatio = smallestContainerSide == rect.Size.X 
            ? (float)smallestContainerSide / sourceRect.Size.X 
            : (float)smallestContainerSide / sourceRect.Size.Y;
          
          return new Point((int)(sourceRect.Size.X * containRatio), (int)(sourceRect.Size.Y * containRatio));
        
        case ImageSizeMode.Cover:
          var smallestSourceSide = Math.Min(sourceRect.Size.X, sourceRect.Size.Y);
          
          var coverRatio = smallestSourceSide == rect.Size.X 
            ? (float)smallestSourceSide / sourceRect.Size.X 
            : (float)smallestSourceSide / sourceRect.Size.Y;
          
          return new Point((int)(sourceRect.Size.X * coverRatio), (int)(sourceRect.Size.Y * coverRatio));
        
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private Point CalculatePosition(Rectangle rect, Point finalSize, HorizontalAlignment hALign, VerticalAlignment vAlign)
    {
      var finalX = 0;
      var finalY = 0;
      
      switch (hALign)
      {
        case HorizontalAlignment.Left:
          finalX = 0;
          break;
        case HorizontalAlignment.Center:
        case HorizontalAlignment.Stretch:
          finalX = (rect.Size.X - finalSize.X) / 2;
          break;
        case HorizontalAlignment.Right:
          finalX = rect.Size.X - finalSize.X;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      
      switch (vAlign)
      {
        case VerticalAlignment.Top:
          finalY = 0;
          break;
        case VerticalAlignment.Middle:
        case VerticalAlignment.Stretch:
          finalY = (rect.Size.Y - finalSize.Y) / 2;
          break;
        case VerticalAlignment.Bottom:
          finalY = rect.Size.Y - finalSize.Y;
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
      
      return new Point(finalX, finalY);
    }

    private void RenderImage(Texture2D texture, Rectangle destRect, Rectangle sourceRect)
    {
      var rect = AreaStack.Peek();
      
      GraphicsManager.DrawCropped(rect, () =>
      {
        GraphicsManager.Draw(texture, sourceRect, destRect);
      });
    }
  }
}