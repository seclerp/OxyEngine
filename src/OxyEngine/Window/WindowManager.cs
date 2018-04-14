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

    public bool GetBorders()
    {
      return !_instance.Window.IsBorderless;
    }

    public int GetWidth()
    {
      return _instance.GraphicsDeviceManager.PreferredBackBufferWidth;
    }

    public int GetHeight()
    {
      return _instance.GraphicsDeviceManager.PreferredBackBufferHeight;
    }
    
    public bool GetFullscreen()
    {
      return _instance.GraphicsDeviceManager.IsFullScreen;
    }

    public string GetTitle()
    {
      return _instance.Window.Title;
    }
    
    public bool GetResizable()
    {
      return _instance.Window.AllowUserResizing;
    }
    
    public bool GetCursorVisible()
    {
      return _instance.IsMouseVisible;
    }
    
    #endregion
    
    #region Set

    public void SetBorders(bool hasBorders = true)
    {
      _instance.Window.IsBorderless = !hasBorders;
    } 
    
    public void SetWidth(int width)
    {
      _instance.GraphicsDeviceManager.PreferredBackBufferWidth = width;
    } 
    
    public void SetHeight(int height)
    {
      _instance.GraphicsDeviceManager.PreferredBackBufferHeight = height;
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
      _instance.GraphicsDeviceManager.IsFullScreen = isFullscreen;
    }
    
    public void SetTitle(string title)
    {
      _instance.Window.Title = title;
    }
    
    public void SetResizable(bool isResizable)
    {
      _instance.Window.AllowUserResizing = isResizable;
    }
    
    public void SetCursorVisible(bool isVisible)
    {
      _instance.IsMouseVisible = isVisible;
    }
    
    #endregion

    public void ApplyChanges()
    {
      _instance.GraphicsDeviceManager.ApplyChanges();
    }
  }
}