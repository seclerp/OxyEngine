using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Oxy.Framework;
using Oxy.Framework.EventManagers;
using Oxy.Framework.Settings;

namespace Oxy.Playground
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
      InitializeModules();
      InitializeOxyState();
      
      // Do not remove
      base.Initialize();
    }

    #region Life time

    protected override void LoadContent()
    {
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
      GraphicsDevice.Clear(Color.CornflowerBlue);

      _lifetimeManager.Draw();

      base.Draw(gameTime);
    }

    #endregion

    private void InitializeModules()
    {
      // Resources
      _resources = new Resources(Content, _project.Settings.ResourcesSettings);
      
      // Graphics
      _defaultSpriteBatch = new SpriteBatch(GraphicsDevice);
      _graphics = new Graphics(_graphicsDeviceManager, _defaultSpriteBatch, _resources, _project.Settings.GraphicsSettings);
      
      // Window
      ApplyWindowSettings(_project.Settings.WindowSettings);
    }

    private void InitializeOxyState()
    {
      Framework.Oxy.Graphics = _graphics;
      Framework.Oxy.Resources = _resources;
    }

    private void ApplyWindowSettings(WindowSettings settings)
    {
      Window.Title = settings.Title;
      Window.AllowUserResizing = settings.Resizable;
      Window.AllowAltF4 = true;
      
      _graphicsDeviceManager.PreferredBackBufferWidth = settings.Width;
      _graphicsDeviceManager.PreferredBackBufferHeight = settings.Height;
      _graphicsDeviceManager.IsFullScreen = settings.IsFullscreen;
    }
  }
}