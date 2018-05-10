using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OxyEngine.Audio;
using OxyEngine.Events;
using OxyEngine.Graphics;
using OxyEngine.Input;
using OxyEngine.Interfaces;
using OxyEngine.Loggers;
using OxyEngine.Projects;
using OxyEngine.Resources;
using OxyEngine.Settings;
using OxyEngine.Window;

namespace OxyEngine
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class GameInstance : Game, IApiProvider
  {
    public GlobalEventsManager Events { get; private set; }
    
    internal readonly GraphicsDeviceManager GraphicsDeviceManager;
    
    // Must be protected, because inherit members may use it
    protected GameProject Project;

    private OxyApi _api;
    
    public GameInstance(GameProject project)
    {
      LogManager.Log("Creating GameInstance...");
      Project = project;
      GraphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = project.ContentFolderPath;
      _api = new OxyApi();
      
      // Events must be initialized before Initialize (because other modules depends on it)
      InitializeEvents();
    }

    public new void Run()
    {
      LogManager.Log("Game loop starting...");
      base.Run();
    }
    
    protected sealed override void Initialize()
    {
      LogManager.Log("Initializing modules...");
      InitializeModules();
      LogManager.Log("Applying project settings...");
      ApplySettings(Project.GameSettings);
      LogManager.Log("Init event started..");
      
      Events.Init();
      
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
      if (_api.Scripting != null)
        throw new Exception($"Scripting has already set to {_api.Scripting.GetType().Name}");
      
      // We not actually initialize scripting here, because Initialize() not called yet
      // see SetScripting
      _api.Scripting = scriptingFrontend;
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
      Events.Load();
    }

    protected override void UnloadContent()
    {
      LogManager.Log("UnLoad event starting...");
      Events.Unload();
    }

    protected override void Update(GameTime gameTime)
    {
      // TODO: Move to input module`
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
          Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      Events.BeginUpdate();
      Events.Update(gameTime.ElapsedGameTime.TotalSeconds);
      Events.EndUpdate();
      
      base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
      Events.BeginDraw();
      Events.Draw();
      Events.EndDraw();

      base.Draw(gameTime);
    }

    #endregion

    #region Initialization
    
    private void InitializeEvents()
    {
      Events = new GlobalEventsManager();
    }
    
    private void InitializeModules()
    {    
      // Resources
      InitializeResources();
      
      // Graphics
      InitializeGraphics();

      // Audio
      InitializeAudio();
      
      // Input
      InitializeInput();

      // Window
      InitializeWindow();
      
      // Scripting
      InitializeScripting();
    }

    private void InitializeResources()
    {
      _api.Resources = new ResourceManager(this, Content, Project.GameSettings.ResourcesSettings);
    }

    private void InitializeGraphics()
    {
      var spriteBatch = new SpriteBatch(GraphicsDevice);
      _api.Graphics = new GraphicsManager(this, GraphicsDeviceManager, spriteBatch, Project.GameSettings.GraphicsSettings);
    }
    
    private void InitializeAudio()
    {
      _api.Audio = new AudioManager();
    }

    private void InitializeInput()
    {
      _api.Input = new InputManager(this);
    }
    
    private void InitializeWindow()
    {
      _api.Window = new WindowManager(this);
    }
    
    private void InitializeScripting()
    {
      // No scripting frontend provided
      if (_api.Scripting == null)
        return;
      
      _api.Scripting.Initialize(Project.ContentFolderPath);
      
      // Add API to scripts
      _api.Scripting.SetGlobal("Oxy", _api);
    }

    #endregion

    private void ApplySettings(GameSettingsRoot settings)
    {
      settings.Apply(this);
    }
  }
}