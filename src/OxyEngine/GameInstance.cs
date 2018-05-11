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

    private ApiManager _apiManager;
    
    public GameInstance(GameProject project)
    {
      LogManager.Log("Creating GameInstance...");
      Project = project;
      GraphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = project.ContentFolderPath;
      _apiManager = new ApiManager();
      
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
      if (_apiManager.Scripting != null)
        throw new Exception($"Scripting has already set to {_apiManager.Scripting.GetType().Name}");
      
      // We not actually initialize scripting here, because Initialize() not called yet
      // see SetScripting
      _apiManager.Scripting = scriptingFrontend;
      LogManager.Log($"Using scripting frontend: {scriptingFrontend.GetType().Name}");
    }

    /// <summary>
    ///   Returns API object that contain engine modules
    /// </summary>
    /// <returns></returns>
    public ApiManager GetApi()
    {
      return _apiManager;
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
      _apiManager.Resources = new ResourceManager(this, Content, Project.GameSettings.ResourcesSettings);
    }

    private void InitializeGraphics()
    {
      var spriteBatch = new SpriteBatch(GraphicsDevice);
      _apiManager.Graphics = new GraphicsManager(this, GraphicsDeviceManager, spriteBatch, Project.GameSettings.GraphicsSettings);
    }
    
    private void InitializeAudio()
    {
      _apiManager.Audio = new AudioManager();
    }

    private void InitializeInput()
    {
      _apiManager.Input = new InputManager(this);
    }
    
    private void InitializeWindow()
    {
      _apiManager.Window = new WindowManager(this);
    }
    
    private void InitializeScripting()
    {
      // No scripting frontend provided
      if (_apiManager.Scripting == null)
        return;
      
      _apiManager.Scripting.Initialize(Project.ContentFolderPath);
      
      // Add API to scripts
      _apiManager.Scripting.SetGlobal("Oxy", _apiManager);
    }

    #endregion

    private void ApplySettings(GameSettingsRoot settings)
    {
      settings.Apply(this);
    }
  }
}