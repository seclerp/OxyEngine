using System;
using System.Diagnostics;
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

    internal Resources Resources;
    internal Graphics Graphics;
    internal IScripting Scripting;
    internal Events Events;
    internal Input Input;

    #endregion

    internal GraphicsDeviceManager _graphicsDeviceManager;
    
    private GameProject _project;
    private SpriteBatch _defaultSpriteBatch;
    private GamePadState[] _gamePadStates;

    private OxyApi _api;
    
    public GameInstance(GameProject project)
    {
      _project = project;
      _graphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = project.ContentFolderPath;
    }

    protected override void Initialize()
    {
      InitializeModules();
      ApplySettings(_project.GameSettings);
      
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
      if (Scripting != null)
        throw new Exception($"Scripting has already set to {Scripting.GetType().Name}");
      
      // We not actually initialize scripting here, because Initialize() not called yet
      // see SetScripting
      Scripting = scriptingFrontend;
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
      // Execute entry script
      Scripting.ExecuteScript(_project.EntryScriptName);
      
      Events.Load();
    }

    protected override void UnloadContent()
    {
      Events.Unload();
      
      // Free resources
      Resources.Dispose();
      Resources = null;
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
          Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      UpdateInput();
      
      Events.Update(gameTime.ElapsedGameTime.TotalSeconds);

      base.Update(gameTime);
    }

    private void UpdateInput()
    {
      UpdateAllGamepadStates();
      Input.UpdateInputState(Keyboard.GetState(), Mouse.GetState(), _gamePadStates);
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
      Graphics.BeginDraw();
      Events.Draw();
      Graphics.EndDraw();

      base.Draw(gameTime);
    }

    #endregion

    private void InitializeModules()
    {
      // Events
      Events = new Events();
      
      // Resources
      Resources = new Resources(Content, _project.GameSettings.ResourcesSettings);
      
      // Graphics
      _defaultSpriteBatch = new SpriteBatch(GraphicsDevice);
      Graphics = new Graphics(_graphicsDeviceManager, _defaultSpriteBatch, _project.GameSettings.GraphicsSettings);
      
      // Input
      Input = new Input();
      
      // Window
      // ...
      
      // API
      InitializeOxyApi();

      //  Scripting
      InitializeScripting();
    }

    private void InitializeOxyApi()
    {
      _api = new OxyApi();
      _api.Resources = Resources;
      _api.Graphics = Graphics;
      _api.Events = Events;
      _api.Input = Input;
    }

    private void InitializeScripting()
    {
      // No scripting frontend provided
      if (Scripting == null)
        return;
      
      Scripting.Initialize(_project.ScriptsFolderPath);
      _api.Scripting = Scripting;
      
      // Add API to scripts
      Scripting.SetGlobal("Oxy", _api);
    }

    private void ApplySettings(GameSettingsRoot settings)
    {
      settings.Apply(this);
    }
  }
}