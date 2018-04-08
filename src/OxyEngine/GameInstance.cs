using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OxyEngine.Events;
using OxyEngine.Graphics;
using OxyEngine.Input;
using OxyEngine.Interfaces;
using OxyEngine.Loggers;
using OxyEngine.Projects;
using OxyEngine.Resources;
using OxyEngine.Settings;

namespace OxyEngine
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class GameInstance : Game, IApiUser
  {
    #region Modules

    private ResourceManager _resourceManager;
    private GraphicsManager _graphicsManager;
    private IScriptingManager _scriptingManager;
    private GlobalEventsManager _eventsManager;
    private InputManager _inputManager;

    #endregion

    internal readonly GraphicsDeviceManager GraphicsDeviceManager;
    protected GameProject Project;

    private SpriteBatch _defaultSpriteBatch;
    private GamePadState[] _gamePadStates;

    private OxyApi _api;
    
    public GameInstance(GameProject project)
    {
      LogManager.Log("Creating GameInstance...");
      Project = project;
      GraphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = project.ContentFolderPath;
      _api = new OxyApi();
      
      // Events must be initialized before Initialize
      InitializeEvents();
    }

    public new void Run()
    {
      LogManager.Log("Game loop starting...");
      base.Run();
    }
    
    protected override void Initialize()
    {
      LogManager.Log("Initializing modules...");
      InitializeModules();
      LogManager.Log("Applying project settings...");
      ApplySettings(Project.GameSettings);
      LogManager.Log("Init event started..");
      
      _eventsManager.Init();
      
      // Do not remove
      base.Initialize();
    }

    #region Public API

    /// <summary>
    ///   Set scripting frontend
    ///   Call this before Run()
    /// </summary>
    /// <param name="scriptingFrontend"></param>
    public void SetScripting(IScriptingManager scriptingFrontend)
    {
      if (_scriptingManager != null)
        throw new Exception($"Scripting has already set to {_scriptingManager.GetType().Name}");
      
      // We not actually initialize scripting here, because Initialize() not called yet
      // see SetScripting
      _scriptingManager = scriptingFrontend;
      LogManager.Log($"Using scripting frontend: {scriptingFrontend.GetType().Name}");
    }

    /// <summary>
    ///   Returns API object that contain engine modules
    /// </summary>
    /// <returns></returns>
    public OxyApi GetApi()
    {
      return _api;
    }
    
    #endregion
    
    #region Life time

    protected override void LoadContent()
    {
      LogManager.Log("Load event starting...");
      _eventsManager.Load();
    }

    protected override void UnloadContent()
    {
      LogManager.Log("UnLoad event starting...");
      _eventsManager.Unload();
      
      // Free resources
      _resourceManager.Dispose();
      _resourceManager = null;
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
          Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      UpdateInput();
      
      _eventsManager.Update(gameTime.ElapsedGameTime.TotalSeconds);

      base.Update(gameTime);
    }

    private void UpdateInput()
    {
      UpdateAllGamepadStates();
      _inputManager.UpdateInputState(Keyboard.GetState(), Mouse.GetState(), _gamePadStates);
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
      _graphicsManager.BeginDraw();
      _eventsManager.Draw();
      _graphicsManager.EndDraw();

      base.Draw(gameTime);
    }

    #endregion

    #region Initialization

    private void InitializeModules()
    {    
      // Resources
      InitializeResources();
      
      // Graphics
      InitializeGraphics();

      // Input
      InitializeInput();

      // Window
      // ...
      
      //  Scripting
      InitializeScripting();
    }

    private void InitializeEvents()
    {
      _eventsManager = new GlobalEventsManager();
      _api.Events = _eventsManager;
    }

    private void InitializeResources()
    {
      _resourceManager = new ResourceManager(Content, Project.GameSettings.ResourcesSettings);
      _api.Resources = _resourceManager;
    }

    private void InitializeGraphics()
    {
      _defaultSpriteBatch = new SpriteBatch(GraphicsDevice);
      _graphicsManager = new GraphicsManager(GraphicsDeviceManager, _defaultSpriteBatch, Project.GameSettings.GraphicsSettings);
      _api.Graphics = _graphicsManager;
    }

    private void InitializeInput()
    {
      _inputManager = new InputManager();
      _api.Input = _inputManager;
    }
    
    private void InitializeScripting()
    {
      // No scripting frontend provided
      if (_scriptingManager == null)
        return;
      
      _scriptingManager.Initialize(Project.ContentFolderPath);
      
      // Add API to scripts
      _scriptingManager.SetGlobal("Oxy", _api);
      _api.Scripting = _scriptingManager;
    }

    #endregion

    private void ApplySettings(GameSettingsRoot settings)
    {
      settings.Apply(this);
    }
  }
}