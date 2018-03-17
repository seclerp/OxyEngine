using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Oxy.Framework;

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

    #endregion
    
    private GraphicsDeviceManager _graphicsDeviceManager;
    private SpriteBatch _defaultSpriteBatch;

    public PlaygroundInstance()
    {
      _graphicsDeviceManager = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
      InitializeModules();
      
      // Do not remove
      base.Initialize();
    }
    
    protected override void LoadContent()
    {
    }

    protected override void UnloadContent()
    {
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
          Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        Exit();
      }

      // TODO: Add your update logic here

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.CornflowerBlue);

      // TODO: Add your drawing code here

      base.Draw(gameTime);
    }

    private void InitializeModules()
    {
      // Resources
      _resources = new Resources(Content);
      _defaultSpriteBatch = new SpriteBatch(GraphicsDevice);
      _graphics = new Graphics(_graphicsDeviceManager, _defaultSpriteBatch, _resources);
    }
  }
}