using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OxyFramework;
using OxyFramework.EventManagers;
using OxyFramework.Settings;

namespace OxyPlayground
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class PlaygroundInstance : Game
  {
    #region Modules

    private Resources _resources;
    private Graphics _graphics;
    private Scripts _scripts;

    #endregion

    private PlaygroundProject _project;
    
    private GameLifetimeEventManager _lifetimeManager;
    
    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _defaultSpriteBatch;

    public PlaygroundInstance(PlaygroundProject project)
    {
      _project = project;
      _graphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = project.ContentFolderPath;
    }

    protected override void Initialize()
    {
      _lifetimeManager = new GameLifetimeEventManager();
      
      InitializeModules();
      InitializeOxyState();
      
      // Do not remove
      base.Initialize();
    }

    #region Life time

    protected override void LoadContent()
    {
      // Execute entry script
      _scripts.ExecuteScript(_project.EntryScriptName);
      
      _lifetimeManager.Load();
    }

    protected override void UnloadContent()
    {
      _lifetimeManager.Unload();
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
          Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      _lifetimeManager.Update(gameTime.ElapsedGameTime.TotalSeconds);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      _graphics.BeginDraw();
      _lifetimeManager.Draw();
      _graphics.EndDraw();

      base.Draw(gameTime);
    }

    #endregion

    private void InitializeModules()
    {
      // Resources
      _resources = new Resources(Content, _project.Settings.ResourcesSettings);
      
      // Graphics
      _defaultSpriteBatch = new SpriteBatch(GraphicsDevice);
      _graphics = new Graphics(_graphicsDeviceManager, _defaultSpriteBatch, _project.Settings.GraphicsSettings);
      
      // Scripts
      _scripts = new Scripts(_project.ScriptsFolderPath);
      
      // Window
      ApplyWindowSettings(_project.Settings.WindowSettings);
    }

    private void InitializeOxyState()
    {
      Oxy.Graphics = _graphics;
      Oxy.Resources = _resources;
      Oxy.Events = _lifetimeManager;
    }

    private void ApplyWindowSettings(WindowSettings settings)
    {
      Window.Title = settings.Title;
      Window.AllowUserResizing = settings.Resizable;
      Window.AllowAltF4 = true;
      Window.IsBorderless = !settings.AllowBorders;
      IsMouseVisible = settings.CursorVisible;

      _graphicsDeviceManager.PreferredBackBufferWidth = settings.Width;
      _graphicsDeviceManager.PreferredBackBufferHeight = settings.Height;
      _graphicsDeviceManager.IsFullScreen = settings.IsFullscreen;

      _graphicsDeviceManager.ApplyChanges();
    }
  }
}