using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Events;
using OxyEngine.Events.Args;
using OxyEngine.Graphics.Extensions;
using OxyEngine.Interfaces;
using OxyEngine.Loggers;
using OxyEngine.Settings;

namespace OxyEngine.Graphics
{
  /// <summary>
  ///   Module that used for rendering all things in engine
  /// </summary>
  public class GraphicsManager : IModule
  {
    #region Constants

    #region Default color bytes
    
    private const byte DefaultColorR = 255;
    private const byte DefaultColorG = 255;
    private const byte DefaultColorB = 255;
    private const byte DefaultColorA = 255;

    private const byte DefaultBgColorR = 100;
    private const byte DefaultBgColorG = 149;
    private const byte DefaultBgColorB = 237;
    private const byte DefaultBgColorA = 255;

    #endregion
    
    #endregion

    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly SpriteBatch _defaultSpriteBatch;
    
    private readonly GraphicsState _currentState;

    #region Initialization

    
    internal GraphicsManager(GameInstance gameInstance, GraphicsDeviceManager graphicsDeviceManager, SpriteBatch defaultSpriteBatch, 
      GraphicsSettings settings)
    {
      _graphicsDeviceManager = graphicsDeviceManager;
      _defaultSpriteBatch = defaultSpriteBatch;
      
      _currentState = new GraphicsState
      {
        BackgroundColor = new Color(DefaultBgColorR, DefaultBgColorG, DefaultBgColorB, DefaultBgColorA),
        ForegroundColor = new Color(DefaultColorR, DefaultColorG, DefaultColorB, DefaultColorA),
        LineWidth = 1,
        TransformationStack = new Stack<Transformation>(),
        RasterizerState = new RasterizerState { ScissorTestEnable = true }
      };

      ClearTransformationStack();
      
      gameInstance.Events.Global.AddListenersUsingAttributes(this);
    }
    
    /// <summary>
    ///   Event listener for global beforedraw event
    /// </summary>
    [ListenEvent(EventNames.Gameloop.Draw.OnBeginDraw)]
    private void OnBeginDraw(object sender, EngineEventArgs args)
    {
      BeginDraw();
    }
    
    /// <summary>
    ///   Event listener for global afterdraw event
    /// </summary>
    [ListenEvent(EventNames.Gameloop.Draw.OnEndDraw)]
    private void OnEndDraw(object sender, EngineEventArgs args)
    {
      EndDraw();
    }
    
    private void ClearTransformationStack()
    {
      _currentState.TransformationStack.Clear();

      // Default transformation
      _currentState.TransformationStack.Push(new Transformation(new Vector2(0, 0), 0, new Vector2(1, 1)));
    }

    private void BeginDraw()
    {
      ClearTransformationStack();
      _graphicsDeviceManager.GraphicsDevice.Clear(GetBackgroundColor());

      _defaultSpriteBatch.Begin(samplerState: SamplerState.LinearWrap);
    }

    private void EndDraw()
    {
      _defaultSpriteBatch.End();
    }

    #endregion

    #region Public API

    #region Fabrics

    /// <summary>
    ///   Creates new RectObject
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="w">Width</param>
    /// <param name="h">Height</param>
    /// <returns></returns>
    public Rectangle NewRect(int x, int y, int w, int h)
    {
      return new Rectangle(x, y, w, h);
    }

    /// <summary>
    ///   Creates empty texture with given resolution
    /// </summary>
    /// <param name="width">Width of new texture</param>
    /// <param name="height">Height of new texture</param>
    /// <returns></returns>
    public Texture2D NewTexture(int width, int height)
    {
      return new Texture2D(_graphicsDeviceManager.GraphicsDevice, width, height);
    }
    
    /// <summary>
    ///   Creates render texture with given resolution
    /// </summary>
    /// <param name="width">Width of render texture</param>
    /// <param name="height">Height of render texture</param>
    /// <returns></returns>
    public RenderTarget2D NewRenderTexture(int width, int height)
    {
      return new RenderTarget2D(
        graphicsDevice: _graphicsDeviceManager.GraphicsDevice, 
        width: width, 
        height: height,
        mipMap: false, 
        preferredFormat: _graphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferFormat,
        preferredDepthFormat: DepthFormat.Depth24
      );
    }

    #endregion

    #region Set

    /// <summary>
    ///   Set drawing color
    ///   Call without parameters to reset to defaults
    /// </summary>
    /// <param name="r">Red color component</param>
    /// <param name="g">Green color component</param>
    /// <param name="b">Blue color component</param>
    /// <param name="a">Alpha color component</param>
    public void SetColor(byte r = DefaultColorR, byte g = DefaultColorG, byte b = DefaultColorB,
      byte a = DefaultColorA)
    {
      _currentState.ForegroundColor = new Color(r, g, b, a);
    }

    /// <summary>
    ///   Set background color
    ///   Call without parameters to reset default color
    /// </summary>
    /// <param name="r">Red color component</param>
    /// <param name="g">Green color component</param>
    /// <param name="b">Blue color component</param>
    /// <param name="a">Alpha color component</param>
    public void SetBackgroundColor(byte r = DefaultBgColorR, byte g = DefaultBgColorG, byte b = DefaultBgColorB,
      byte a = DefaultBgColorA)
    {
      _currentState.BackgroundColor = new Color(r, g, b, a);
    }

    /// <summary>
    ///   Set line thickness for drawing primitives
    /// </summary>
    /// <param name="lineWidth">Thickness (>= 1)</param>
    /// <exception cref="Exception">Fires when thickness is lower than 1</exception>
    public void SetLineWidth(float lineWidth)
    {
      _currentState.LineWidth = lineWidth;
    }

    /// <summary>
    ///   Sets target for drawing anything to a RenderTarget2D (also known as render texture) 
    /// </summary>
    /// <param name="texture">Render texture to draw on</param>
    public void SetRenderTexture(RenderTarget2D texture = null)
    {
      _graphicsDeviceManager.GraphicsDevice.SetRenderTarget(texture);
      _graphicsDeviceManager.GraphicsDevice.Clear(Color.Transparent);
    }

    /// <summary>
    ///   Set current font for printing text
    /// </summary>
    /// <param name="font">Font to use for printing</param>
    public void SetFont(SpriteFont font)
    {
      _currentState.Font = font;
    }

    /// <summary>
    ///   Set cropping drawing rectangle
    ///   null to reset it to default state
    /// </summary>
    /// <param name="cropping">Cropping drawing rectangle</param>
    public void SetCropping(Rectangle? cropping = null)
    {
      _currentState.BackupCropping = _graphicsDeviceManager.GraphicsDevice.ScissorRectangle;
        
      if (cropping is null)
      {
        _graphicsDeviceManager.GraphicsDevice.ScissorRectangle = _currentState.BackupCropping;
        return;
      }

      _graphicsDeviceManager.GraphicsDevice.ScissorRectangle = cropping.Value;
    }
    
    #endregion

    #region Get

    /// <summary>
    ///   Returns main drawing color
    /// </summary>
    /// <returns>Current foreground color</returns>
    public Color GetColor()
    {
      return _currentState.ForegroundColor;
    }

    /// <summary>
    ///   Returns background color
    /// </summary>
    /// <returns>Current background color</returns>
    public Color GetBackgroundColor()
    {
      return _currentState.BackgroundColor;
    }

    /// <summary>
    ///   Returns primitive's line thickness
    /// </summary>
    /// <returns>Current line width</returns>
    public float GetLineWidth()
    {
      return _currentState.LineWidth;
    }

    /// <summary>
    ///   Returns render target or null if it is not set
    /// </summary>
    /// <returns>render target or null if it is not set</returns>
    public RenderTarget2D GetRenderTexture()
    {
      if (_graphicsDeviceManager.GraphicsDevice.RenderTargetCount == 0)
      {
        return null;
      }

      var targets = new RenderTargetBinding[_graphicsDeviceManager.GraphicsDevice.RenderTargetCount];
      _graphicsDeviceManager.GraphicsDevice.GetRenderTargets(targets);
      
      return targets[0].RenderTarget as RenderTarget2D;
    }

    /// <summary>
    ///   Returns current font used by printing
    /// </summary>
    /// <returns>current font used by printing</returns>
    public SpriteFont GetFont()
    {
      return _currentState.Font;
    }

    /// <summary>
    ///   Returns current cropping draw rectangle
    /// </summary>
    /// <returns></returns>
    public Rectangle GetCropping()
    {
      return _graphicsDeviceManager.GraphicsDevice.ScissorRectangle;
    }

    #endregion

    #region Drawing

    /// <summary>
    ///   Clears screen or rendertarget with given color
    /// </summary>
    /// <param name="color">New background color</param>
    public void Clear(Color color)
    {
      _graphicsDeviceManager.GraphicsDevice.Clear(color);
    }
    
    /// <summary>
    ///   Prints text on screen
    /// </summary>
    /// <param name="text">Text to print</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="ox">X offset</param>
    /// <param name="oy">Y offset</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Print(string text, float x = 0, float y = 0, float ox = 0, float oy = 0, float r = 0, float sx = 1,
      float sy = 1)
    {
      _defaultSpriteBatch.DrawString(_currentState.Font, text, new Vector2(x, y), _currentState.ForegroundColor, r, new Vector2(ox, oy), 
        new Vector2(sx, sy), SpriteEffects.None, 0);
    }
    
    /// <summary>
    ///   Draw texture on the screen with given position, rotation and scale
    /// </summary>
    /// <param name="texture">Object to draw</param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="ox">X offset</param>
    /// <param name="oy">Y offset</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Draw(Texture2D texture, float x = 0, float y = 0, float ox = 0, float oy = 0, float r = 0, float sx = 1,
      float sy = 1)
    {
      _defaultSpriteBatch.Draw(texture, new Vector2(x, y), null, _currentState.ForegroundColor, r, new Vector2(ox, oy), 
        new Vector2(sx, sy), SpriteEffects.None, 0);
    }

    /// <summary>
    ///   Draw texture on the screen with given quad, position, rotation and scale
    /// </summary>
    /// <param name="texture">Texture to draw</param>
    /// <param name="sourceRect">Texture rectangle</param>
    /// <param name="destRect">Pixel rectangle</param>
    /// <param name="ox">X offset</param>
    /// <param name="oy">Y offset</param>
    /// <param name="r">Rotation</param>
    public void Draw(Texture2D texture, Rectangle sourceRect, Rectangle destRect, float ox = 0, float oy = 0, float r = 0)
    {
      _defaultSpriteBatch.Draw(texture, destRect, sourceRect, _currentState.ForegroundColor, r, new Vector2(ox, oy), 
        SpriteEffects.None, 0);
    }

    /// <summary>
    ///   Draw rectangle with given style, position of top left corner and size
    /// </summary>
    /// <param name="style">"fill" or "line" draw style</param>
    /// <param name="x">X coodrinate of top left corner</param>
    /// <param name="y">Y coodrinate of top left corner</param>
    /// <param name="w">Width of rectangle</param>
    /// <param name="h">Height of rectangle</param>
    /// <exception cref="Exception">If given style is not supported</exception>
    public void Rectangle(string style, int x, int y, int w, int h)
    {
      Rectangle(style, new Rectangle(x, y, w, h));
    }
    
    /// <summary>
    ///   Draw rectangle with given style, position of top left corner and size
    /// </summary>
    /// <param name="style">"fill" or "line" draw style</param>
    /// <param name="rect">X coodrinate of top left corner</param>
    /// <exception cref="Exception">If given style is not supported</exception>
    public void Rectangle(string style, Rectangle rect)
    {
      switch (style)
      {
        case "fill":
          _defaultSpriteBatch.FillRectangle(rect, _currentState.ForegroundColor);
          break;
        case "line":
          _defaultSpriteBatch.DrawRectangle(rect, _currentState.ForegroundColor, _currentState.LineWidth);
          break;
        default:
          throw new Exception($"Unknown rectangle style: {style}");
      }
    }
    
    /// <summary>
    ///   Draw lines between 2 points
    /// </summary>
    /// <param name="x1">X coodrinate of first point</param>
    /// <param name="y1">Y coodrinate of second point</param>
    /// <param name="x2">X coodrinate of first point</param>
    /// <param name="y2">Y coodrinate of second point</param>
    public void Line(float x1, float y1, float x2, float y2)
    {
      Line(new Vector2(x1, y1), new Vector2(x2, y2));
    }
    
    /// <summary>
    ///   Draw lines between 2 points
    /// </summary>
    /// <param name="a">First point</param>
    /// <param name="b">Second point</param>
    public void Line(Vector2 a, Vector2 b)
    {
      _defaultSpriteBatch.DrawLine(a, b, _currentState.ForegroundColor, _currentState.LineWidth);
    }
    
    /// <summary>
    ///   Draw circle
    /// </summary>
    /// <param name="x">X coordinate of center</param>
    /// <param name="y">Y coordinate of center</param>
    /// <param name="radius">Radius</param>
    public void Circle(float x, float y, float radius)
    {
      Circle(new Vector2(x, y), radius);
    }
    
    /// <summary>
    ///   Draw circle
    /// </summary>
    /// <param name="center">Center point</param>
    /// <param name="radius">Radius</param>
    public void Circle(Vector2 center, float radius)
    {
      var sides = (int) Math.Ceiling(radius / 3) * 3;
      _defaultSpriteBatch.DrawCircle(center, radius, sides, _currentState.ForegroundColor, _currentState.LineWidth);
    }

    /// <summary>
    ///   Draw polygon 
    /// </summary>
    /// <param name="points">List of points</param>
    public void Polygon(List<Vector2> points)
    {
      _defaultSpriteBatch.DrawPolygon(Vector2.Zero, points, _currentState.ForegroundColor, _currentState.LineWidth);
    }
    
    /// <summary>
    ///   Draw polygon
    /// </summary>
    /// <param name="offset">Offset for point coodrinates</param>
    /// <param name="points">List of points</param>
    public void Polygon(Vector2 offset, List<Vector2> points)
    {
      _defaultSpriteBatch.DrawPolygon(offset, points, _currentState.ForegroundColor, _currentState.LineWidth);
    }

    /// <summary>
    ///   Draw point
    /// </summary>
    /// <param name="x">X coodrinate of a point</param>
    /// <param name="y">Y coodrinate of a point</param>
    public void Point(float x, float y)
    {
      _defaultSpriteBatch.PutPixel(new Vector2(x, y), _currentState.ForegroundColor);
    }

    /// <summary>
    ///   Draws things on target using action
    /// </summary>
    /// <param name="target">Render texture to draw on</param>
    /// <param name="action">Action that draws</param>
    public void DrawOn(RenderTarget2D target, Action action)
    {
      SetRenderTexture(target);
      _graphicsDeviceManager.GraphicsDevice.Clear(Color.Transparent);
      _defaultSpriteBatch.End();
      _defaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.LinearWrap);
      action();
      _defaultSpriteBatch.End();
      SetRenderTexture();
      _defaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.LinearWrap);
    }

    // TODO
    public void DrawCropped(Rectangle cropRectangle, Action action)
    {
      _defaultSpriteBatch.End();
      _defaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearWrap, rasterizerState: _currentState.RasterizerState);
      
      SetCropping(cropRectangle);
      action();
      SetCropping();
      
      _defaultSpriteBatch.End();
      _defaultSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.LinearWrap);
    }

    #endregion

    #region Matrix transformations

    /// <summary>
    ///   Pushes matrix stack
    /// </summary>
    public void PushMatrix()
    {
      var currentMatrix = _currentState.TransformationStack.Peek();
      var newMatrix = (Transformation)currentMatrix.Clone();
      _currentState.TransformationStack.Push(newMatrix);
    }

    /// <summary>
    ///   Pops matrix stack
    /// </summary>
    public void PopMatrix()
    {
      if (_currentState.TransformationStack.Count == 1)
        throw new Exception("Can't pop last matrix state");
      
      var poped = _currentState.TransformationStack.Pop();
    }

    /// <summary>
    ///   Moves coordinate system among X and Y axis
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    public void Translate(float x, float y)
    {
      _defaultSpriteBatch.End();
      _currentState.TransformationStack.Peek().Translate(new Vector2(x, y));
      _defaultSpriteBatch.Begin(transformMatrix: _currentState.TransformationStack.Peek().Matrix, 
        samplerState: SamplerState.LinearWrap);
    }

    /// <summary>
    ///   Rotates coordinate system by given angle
    /// </summary>
    /// <param name="r">Angle to rotate</param>
    public void Rotate(float r)
    {
      _defaultSpriteBatch.End();
      _currentState.TransformationStack.Peek().Rotate(r);
      _defaultSpriteBatch.Begin(transformMatrix: _currentState.TransformationStack.Peek().Matrix, 
        samplerState: SamplerState.LinearWrap);
    }

    /// <summary>
    ///   Scales coordinate system
    /// </summary>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Scale(float sx, float sy)
    {
      _defaultSpriteBatch.End();
      _currentState.TransformationStack.Peek().Zoom(new Vector2(sx, sy));
      _defaultSpriteBatch.Begin(transformMatrix: _currentState.TransformationStack.Peek().Matrix, 
        samplerState: SamplerState.LinearWrap);
    }

    /// <summary>
    ///   Scales coordinate system
    /// </summary>
    /// <param name="s">X and Y scale factor</param>
    public void Scale(float s)
    {
      Scale(s, s);
    }

    #endregion

    #endregion
  }
}