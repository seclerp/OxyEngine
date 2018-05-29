using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Graphics;
using OxyEngine.GUI.Models;
using OxyEngine.GUI.States;
using OxyEngine.GUI.Styles;

namespace OxyEngine.GUI.Renderers
{
  public class GuiRenderer : Renderer
  {
    private readonly TextRenderer _textRenderer;
    private readonly ImageRenderer _imageRenderer;
    
    public GuiRenderer(AreaStack areaStack, StyleDatabase styles) : base(areaStack, styles)
    {
      _textRenderer = new TextRenderer(areaStack, styles);
      _imageRenderer = new ImageRenderer(areaStack, styles);
    }
    
    public void FreeLayout(Rectangle rectangle, Action<GuiRenderer> action = null, Style style = null)
    {
      style = style ?? StyleDatabase.DefaultStyle;
      
      AreaStack.Push(rectangle);
      GraphicsManager.DrawCropped(AreaStack.Peek(), () =>
      {
        BackgroundLayer(AreaStack.Peek(), style);
        action?.Invoke(this);
        BordersLayer(AreaStack.Peek(), style);
      });
      AreaStack.Pop();
    }
    
    public Rectangle Panel(Rectangle rect, string title = "Panel", 
      Action<GuiRenderer> action = null, Style headerStyle = null,  Style panelStyle = null)
    {
      Text(new Rectangle(rect.X, rect.Y, rect.Width, 25)
        , title
        , headerStyle
      );
      FreeLayout(new Rectangle(rect.X, rect.Y + 25, rect.Width, rect.Height)
        , action
        , panelStyle
      );
      
      // TODO: Movement, buttons logic

      return rect;
    }
    
    public void Text(Rectangle rect, string text, Style style = null)
    {
      style = style ?? StyleDatabase.DefaultStyle;
      
      AreaStack.Push(rect);
      GraphicsManager.DrawCropped(AreaStack.Peek(), () =>
      {
        BackgroundLayer(AreaStack.Peek(), style);
        _textRenderer.Render(AreaStack.Peek(), text, style);
        BordersLayer(AreaStack.Peek(), style);
      });
      AreaStack.Pop();
    }
    
    public void Image(Texture2D texture, Rectangle rect, Style style = null)
    {
      style = style ?? StyleDatabase.DefaultStyle;
      
      AreaStack.Push(rect);
      GraphicsManager.DrawCropped(AreaStack.Peek(), () =>
      {
        BackgroundLayer(AreaStack.Peek(), style);
        _imageRenderer.Render(texture, AreaStack.Peek(), style);
        BordersLayer(AreaStack.Peek(), style);
      });
      AreaStack.Pop();
    }

    public ButtonState Button(Rectangle rect, string text, ButtonState buttonState = default, Style style = null, Style textStyle = null)
    {
      style = style ?? StyleDatabase.DefaultStyle;
      textStyle = textStyle ?? StyleDatabase.DefaultStyle;

      var backImageNormal = style.GetRule<Texture2D>("background-image") ?? EmptyTexture2D;
      var backImageHover = style.GetState("hover").GetRule<Texture2D>("background-image") ?? EmptyTexture2D;
      var backImagePressed = style.GetState("pressed").GetRule<Texture2D>("background-image") ?? EmptyTexture2D;

      var isPressed = false;
      var isMouseOver = false;
      var mousePosition = InputManager.GetCursorPosition();

      AreaStack.Push(rect);
      GraphicsManager.DrawCropped(AreaStack.Peek(), () =>
      {
        BackgroundLayer(AreaStack.Peek(), style);
        if (AreaStack.Peek().Contains(mousePosition) && InputManager.IsMouseDown("left"))
        {
          isPressed = true;
          isMouseOver = true;
          _imageRenderer.Render(backImagePressed, AreaStack.Peek(), style.GetState("pressed"), isBackground: true);
        }
        else
        {
          if (AreaStack.Peek().Contains(mousePosition))
          {
            isMouseOver = true;
            _imageRenderer.Render(backImageHover, AreaStack.Peek(), style.GetState("hover"), isBackground: true);
          }
          else
          {
            _imageRenderer.Render(backImageNormal, AreaStack.Peek(), style, isBackground: true);
          }
        }
        
        _textRenderer.Render(AreaStack.Peek(), text, textStyle);

        BordersLayer(AreaStack.Peek(), style);
        
      });
      AreaStack.Pop();
      
      return new ButtonState(isPressed, !buttonState.IsClicked && isPressed, isMouseOver);
    }
    
    private void BackgroundLayer(Rectangle rect, Style style)
    {
      var backColor = style.GetRule<Color>("background-color");
      
      var beforeColor = GraphicsManager.GetColor();
      GraphicsManager.SetColor(backColor.R, backColor.G, backColor.B, backColor.A);
      GraphicsManager.Rectangle("fill", rect.X, rect.Y, rect.Width, rect.Height);
      GraphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
    }
    
    private void BordersLayer(Rectangle rect, Style style)
    {
      var borderColor = style.GetRule<Color>("border-color");
      var borderWidth = style.GetRule<int>("border-width");
      
      var beforeColor = GraphicsManager.GetColor();
      var beforeLineWidth = GraphicsManager.GetLineWidth();
      GraphicsManager.SetColor(borderColor.R, borderColor.G, borderColor.B, borderColor.A);
      GraphicsManager.SetLineWidth(borderWidth);
      GraphicsManager.Rectangle("line", rect.X, rect.Y, rect.Width, rect.Height);
      GraphicsManager.SetLineWidth(beforeLineWidth);
      GraphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
    }
  }
}