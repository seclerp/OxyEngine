using Microsoft.Xna.Framework;
using OxyEngine.Interfaces;

namespace OxyEngine.Window
{
  public class WindowManager : IModule
  {
    private readonly GameInstance _instance;

    public WindowManager(GameInstance instance)
    {
      _instance = instance;
    }

    #region Get

    /// <summary>
    ///   Returns true if window has borders, otherwise false
    /// </summary>
    /// <returns>true if window has borders</returns>
    public bool GetBorders()
    {
      return !(_instance as Game).Window.IsBorderless;
    }

    /// <summary>
    ///   Returns window's width
    /// </summary>
    /// <returns>Window's width</returns>
    public int GetWidth()
    {
      return _instance.GraphicsDeviceManager.PreferredBackBufferWidth;
    }

    /// <summary>
    ///   Returns window's height
    /// </summary>
    /// <returns>Window's height</returns>
    public int GetHeight()
    {
      return _instance.GraphicsDeviceManager.PreferredBackBufferHeight;
    }
    
    /// <summary>
    ///   Returns true if fullscreen mode enabled, otherwise false
    /// </summary>
    /// <returns>true if fullscreen mode enabled, otherwise false</returns>
    public bool GetFullscreen()
    {
      return _instance.GraphicsDeviceManager.IsFullScreen;
    }

    /// <summary>
    ///   Returns window's title
    /// </summary>
    /// <returns>Window's title</returns>
    public string GetTitle()
    {
      return (_instance as Game).Window.Title;
    }
    
    /// <summary>
    ///   Returns true if window could be resized by user, otherwise false
    /// </summary>
    /// <returns>true if window could be resized by user</returns>
    public bool GetResizable()
    {
      return (_instance as Game).Window.AllowUserResizing;
    }
    
    /// <summary>
    ///   Returns true if mouse cursor is visible, otherwise false
    /// </summary>
    /// <returns>true if mouse cursor is visible, otherwise false</returns>
    public bool GetCursorVisible()
    {
      return _instance.IsMouseVisible;
    }
    
    #endregion
    
    #region Set

    /// <summary>
    ///   Enable or disable borders for the window
    /// </summary>
    /// <param name="hasBorders">true to enable borders, otherwise false</param>
    public void SetBorders(bool hasBorders = true)
    {
      (_instance as Game).Window.IsBorderless = !hasBorders;
    } 
    
    /// <summary>
    ///   Sets window's width
    /// </summary>
    /// <param name="width">New window's width</param>
    public void SetWidth(int width)
    {
      _instance.GraphicsDeviceManager.PreferredBackBufferWidth = width;
    } 
    
    /// <summary>
    ///   Sets window's height
    /// </summary>
    /// <param name="height">New window's height</param>
    public void SetHeight(int height)
    {
      _instance.GraphicsDeviceManager.PreferredBackBufferHeight = height;
    }
    
    /// <summary>
    ///   Enable or disable fullscreen mode
    /// </summary>
    /// <param name="isFullscreen">true to enable fullscreen, otherwise false</param>
    public void SetFullscreen(bool isFullscreen = true)
    {
      _instance.GraphicsDeviceManager.IsFullScreen = isFullscreen;
    }
    
    /// <summary>
    ///   Set new window's title
    /// </summary>
    /// <param name="title">New window's title</param>
    public void SetTitle(string title)
    {
      (_instance as Game).Window.Title = title;
    }
    
    /// <summary>
    ///   Enable or disable window resize for user
    /// </summary>
    /// <param name="isResizable">true to enable resize, otherwise false</param>
    public void SetResizable(bool isResizable = true)
    {
      (_instance as Game).Window.AllowUserResizing = isResizable;
    }
    
    /// <summary>
    ///   Set cursor visible or hidden
    /// </summary>
    /// <param name="isVisible">true to make cursor visible, otherwise false</param>
    public void SetCursorVisible(bool isVisible)
    {
      _instance.IsMouseVisible = isVisible;
    }
    
    #endregion

    /// <summary>
    ///   Apply changes to graphics device
    /// </summary>
    public void ApplyChanges()
    {
      _instance.GraphicsDeviceManager.ApplyChanges();
    }
  }
}