using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OxyEngine.Interfaces;
using OxyEngine.Settings;

namespace OxyEngine
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class GameInstance : Game
  {
    #region Modules

    private Resources _resources;
    private Graphics _graphics;
    private IScripting _scripting;
    private Events _events;
    private Input _input;

    #endregion

    private GameProject _project;
    
    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _defaultSpriteBatch;
    private GamePadState[] _gamePadStates;
    
    public GameInstance(GameProject project)
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
      // Execute entry script
      _scripting.ExecuteScript(_project.EntryScriptName);
      
      _events.Load();
    }

    protected override void UnloadContent()
    {
      _events.Unload();
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
          Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      UpdateInput();
      
      _events.Update(gameTime.ElapsedGameTime.TotalSeconds);

      base.Update(gameTime);
    }

    private void UpdateInput()
    {
      UpdateAllGamepadStates();
      _input.UpdateInputState(Keyboard.GetState(), Mouse.GetState(), _gamePadStates);
    }

    private void UpdateAllGamepadStates()
    {
      var count = GamePad.MaximumGamePadCount;

      if (_gamePadStates == null)
        _gamePadStates = new GamePadState[count];
      
      for (int i = 0; i < count; i++)
      {
        _gamePadStates[i] = GamePad.GetState(i);
      }
    }
    
    protected override void Draw(GameTime gameTime)
    {
      _graphics.BeginDraw();
      _events.Draw();
      _graphics.EndDraw();

      base.Draw(gameTime);
    }

    #endregion

    private void InitializeModules()
    {
      // Events
      _events = new Events();
      
      // Resources
      _resources = new Resources(Content, _project.GameSettings.ResourcesSettings);
      
      // Graphics
      _defaultSpriteBatch = new SpriteBatch(GraphicsDevice);
      _graphics = new Graphics(_graphicsDeviceManager, _defaultSpriteBatch, _project.GameSettings.GraphicsSettings);
      
      // Input
      _input = new Input();
      
      // Window
      ApplyWindowSettings(_project.GameSettings.WindowSettings);
    }

    private void InitializeOxyState()
    {
      Oxy.Resources = _resources;
      Oxy.Graphics = _graphics;
      Oxy.Events = _events;
      Oxy.Input = _input;
    }

    public void InitializeScripting(IScripting scriptingFrontend)
    {
      _scripting = scriptingFrontend;
      Oxy.Scripting = _scripting;
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