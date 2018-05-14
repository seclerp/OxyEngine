using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Audio;
using OxyEngine.Dependency;
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
  ///   Base class for game entry point
  /// </summary>
  public class GameInstance : Game
  {
    public GlobalEventsManager Events { get; private set; }
    
    public ResourceManager Resources => _resources;
    public GraphicsManager Graphics => _graphics;
    public InputManager Input => _input;
    public AudioManager Audio => _audio;
    public new WindowManager Window => _window;
    public IScriptingManager Scripting => _scripting;
    
    internal readonly GraphicsDeviceManager GraphicsDeviceManager;
    
    // Must be protected, because inherit members may use it
    protected GameProject Project;

    private ResourceManager _resources;
    private GraphicsManager _graphics;
    private AudioManager _audio;
    private InputManager _input;
    private WindowManager _window;
    private IScriptingManager _scripting;
    
    public GameInstance(GameProject project)
    {
      LogManager.Log("Creating GameInstance...");
      Project = project;
      GraphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = project.ContentFolderPath;
      
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
      
      LogManager.Log("Registering modules...");
      RegisterModules();
      
      LogManager.Log("Applying project settings...");
      ApplySettings(Project.GameSettings);
      
      LogManager.Log("Init event started...");
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
      if (_scripting != null)
        throw new Exception($"Scripting has already set to {_scripting.GetType().Name}");
      
      // We not actually initialize scripting here, because Initialize() not called yet
      // see SetScripting
      _scripting = scriptingFrontend;
      LogManager.Log($"Using scripting frontend: {scriptingFrontend.GetType().Name}");
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
      _resources = new ResourceManager(this, Content, Project.GameSettings.ResourcesSettings);
    }

    private void InitializeGraphics()
    {
      var spriteBatch = new SpriteBatch(GraphicsDevice);
      _graphics = new GraphicsManager(this, GraphicsDeviceManager, spriteBatch, Project.GameSettings.GraphicsSettings);
    }
    
    private void InitializeAudio()
    {
      _audio = new AudioManager();
    }

    private void InitializeInput()
    {
      _input = new InputManager(this);
    }
    
    private void InitializeWindow()
    {
      _window = new WindowManager(this);
    }
    
    private void InitializeScripting()
    {
      _scripting?.Initialize(Project.ContentFolderPath);
    }

    private void SetScriptingGlobals()
    {      
      if (_scripting is null)
      {
        return;
      }
      
      _scripting.SetGlobal(InstanceName.ResourceManager, _resources);
      _scripting.SetGlobal(InstanceName.GraphicsManager, _graphics);
      _scripting.SetGlobal(InstanceName.InputManager, _input);
      _scripting.SetGlobal(InstanceName.AudioManager, _audio);
      _scripting.SetGlobal(InstanceName.WindowManager, _window);
      _scripting.SetGlobal(InstanceName.ScriptingManager, _scripting);
    }

    private void RegisterModules()
    {
      Container.Instance.RegisterByName(InstanceName.ResourceManager, _resources);
      Container.Instance.RegisterByName(InstanceName.GraphicsManager, _graphics);
      Container.Instance.RegisterByName(InstanceName.InputManager, _input);
      Container.Instance.RegisterByName(InstanceName.AudioManager, _audio);
      Container.Instance.RegisterByName(InstanceName.WindowManager, _window);
      Container.Instance.RegisterByName(InstanceName.ScriptingManager, _scripting);
      
      // Add API to scripting
      SetScriptingGlobals();
    }

    #endregion

    private void ApplySettings(GameSettingsRoot settings)
    {
      settings.Apply(this);
    }
  }
}