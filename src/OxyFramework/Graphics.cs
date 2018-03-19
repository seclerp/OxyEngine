using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyFramework.Settings;

namespace OxyFramework
{
  /// <summary>
  ///   Module that used for rendering all things in engine
  /// </summary>
  public class Graphics : IModule
  {
    #region Constants

    private const byte DefaultColorR = 255;
    private const byte DefaultColorG = 255;
    private const byte DefaultColorB = 255;
    private const byte DefaultColorA = 255;

    private const byte DefaultBgColorR = 100;
    private const byte DefaultBgColorG = 149;
    private const byte DefaultBgColorB = 237;
    private const byte DefaultBgColorA = 255;

    #endregion

    private readonly GraphicsDeviceManager _graphicsDeviceManager;
    private readonly SpriteBatch _defaultSpriteBatch;
    
    private readonly GraphicsState _currentState;

    #region Initialization

    public Graphics(GraphicsDeviceManager graphicsDeviceManager, SpriteBatch defaultSpriteBatch, 
      GraphicsSettings settings)
    {
      _graphicsDeviceManager = graphicsDeviceManager;
      _defaultSpriteBatch = defaultSpriteBatch;
      
      _currentState = new GraphicsState
      {
        BackgroundColor = new Color(DefaultBgColorR, DefaultBgColorG, DefaultBgColorB, DefaultBgColorA),
        ForegroundColor = new Color(DefaultColorR, DefaultColorG, DefaultColorB, DefaultColorA),
        LineWidth = 1,
        TransformationStack = new Stack<Transformation>()
      };

      ClearTransformationStack();
    }

    private void ClearTransformationStack()
    {
      _currentState.TransformationStack.Clear();

      // Default transformation
      _currentState.TransformationStack.Push(new Transformation(new Vector2(0, 0), 0, new Vector2(1, 1)));
    }

    public void BeginDraw()
    {
      ClearTransformationStack();
      _graphicsDeviceManager.GraphicsDevice.Clear(GetBackgroundColor());

      _defaultSpriteBatch.Begin();
    }

    public void EndDraw()
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
    public Texture2D NewRenderTexture(int width, int height)
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
    /// <param name="texture"></param>
    public void SetRenderTexture(RenderTarget2D texture = null)
    {
      _graphicsDeviceManager.GraphicsDevice.SetRenderTarget(texture);
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

    #endregion

    #region Drawing

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
      _defaultSpriteBatch.Draw(texture, new Vector2(x, y), null, 
        _currentState.ForegroundColor, r, new Vector2(ox, oy), 
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
      
      _currentState.TransformationStack.Pop();
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
      _defaultSpriteBatch.Begin(transformMatrix: _currentState.TransformationStack.Peek().Matrix);
    }

    /// <summary>
    ///   Rotates coordinate system by given angle
    /// </summary>
    /// <param name="r">Angle to rotate</param>
    public void Rotate(float r)
    {
      _defaultSpriteBatch.End();
      _currentState.TransformationStack.Peek().Rotate(r);
      _defaultSpriteBatch.Begin(transformMatrix: _currentState.TransformationStack.Peek().Matrix);
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
      _defaultSpriteBatch.Begin(transformMatrix: _currentState.TransformationStack.Peek().Matrix);
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