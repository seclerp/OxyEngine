using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OxyEngine.Interfaces;
using OxyEngine.Loggers;
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

    internal readonly GraphicsDeviceManager GraphicsDeviceManager;
    
    private GameProject _project;
    private SpriteBatch _defaultSpriteBatch;
    private GamePadState[] _gamePadStates;

    private OxyApi _api;
    
    public GameInstance(GameProject project)
    {
      LogManager.Log("Creating GameInstance...");
      _project = project;
      GraphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = project.ContentFolderPath;
      _api = new OxyApi();
      
      // Events must be initialized before Initialize
      InitializeEvents();
    }

    public new void Run()
    {
      try
      {
        LogManager.Log("Game loop starting...");
        base.Run();
      }
      catch (Exception e)
      {
        LogManager.Error(e.ToString());
        throw new Exception(e.Message, e);
      }
    }
    
    protected override void Initialize()
    {
      LogManager.Log("Initializing modules...");
      InitializeModules();
      LogManager.Log("Applying project settings...");
      ApplySettings(_project.GameSettings);
      
      _events.BeforeLoad();
      
      // Do not remove
      base.Initialize();
    }

    #region Public API

    /// <summary>
    ///   Set scripting frontend
    ///   Call this before Run()
    /// </summary>
    /// <param name="scriptingFrontend"></param>
    public void SetScripting(IScripting scriptingFrontend)
    {
      if (_scripting != null)
        throw new Exception($"Scripting has already set to {_scripting.GetType().Name}");
      
      // We not actually initialize scripting here, because Initialize() not called yet
      // see SetScripting
      _scripting = scriptingFrontend;
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
      LogManager.Log("Load content started");
      _events.Load();
    }

    protected override void UnloadContent()
    {
      _events.Unload();
      
      // Free resources
      _resources.Dispose();
      _resources = null;
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
      _events = new Events();
      _api.Events = _events;
    }

    private void InitializeResources()
    {
      _resources = new Resources(Content, _project.GameSettings.ResourcesSettings);
      _api.Resources = _resources;
    }

    private void InitializeGraphics()
    {
      _defaultSpriteBatch = new SpriteBatch(GraphicsDevice);
      _graphics = new Graphics(GraphicsDeviceManager, _defaultSpriteBatch, _project.GameSettings.GraphicsSettings);
      _api.Graphics = _graphics;
    }

    private void InitializeInput()
    {
      _input = new Input();
      _api.Input = _input;
    }
    
    private void InitializeScripting()
    {
      // No scripting frontend provided
      if (_scripting == null)
        return;
      
      _scripting.Initialize(_project.ScriptsFolderPath);
      
      // Add API to scripts
      _scripting.SetGlobal("Oxy", _api);
      _api.Scripting = _scripting;
    }

    private void ApplySettings(GameSettingsRoot settings)
    {
      settings.Apply(this);
    }
  }
}