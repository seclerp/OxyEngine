using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;
using OxyEngine.Events;
using OxyEngine.Events.Args;

namespace OxyEngine.Ecs.Systems
{
  /// <summary>
  ///   Class for managing gamesystems
  /// </summary>
  public class GameSystemManager : IInitializable, ILoadable, IUpdateable, IDrawable
  {
    /// <summary>
    ///   System for update and initialization
    /// </summary>
    public GenericSystem GenericSystem { get; private set; }
    
    /// <summary>
    ///   System for drawing
    /// </summary>
    public DrawSystem DrawSystem { get; private set; }

    private GameInstance _gameInstance;

    public GameSystemManager(GameInstance gameInstance)
    {
      _gameInstance = gameInstance;
    }

    /// <summary>
    ///   Attaches listeners to global event for handling
    /// </summary>
    public void InitializeEventListeners()
    {
      _gameInstance.Events.Global.StartListening(EventNames.Initialization.OnInit, 
        (sender, args) => Init()
      );
      _gameInstance.Events.Global.StartListening(EventNames.Initialization.OnLoad, 
        (sender, args) => Load()
      );
      _gameInstance.Events.Global.StartListening(EventNames.Gameloop.Update.OnUpdate, 
        (sender, args) => Update((float)((EngineUpdateEventArgs)args).DeltaTime)
      );
      _gameInstance.Events.Global.StartListening(EventNames.Gameloop.Draw.OnDraw, 
        (sender, args) => Draw()
      );
    }

    /// <summary>
    ///   Initializes systems
    /// </summary>
    public void InitializeSystems(GameEntity rootEntity)
    {     
      GenericSystem = new GenericSystem(rootEntity);
      DrawSystem = new DrawSystem(rootEntity);
    }

    public void Init()
    {
      GenericSystem?.Init();
    }
    
    public void Load()
    {
      GenericSystem?.Load();
    }
    
    public void Update(float dt)
    {
      GenericSystem?.Update(dt);
    }

    public void Draw()
    {
      DrawSystem?.Draw();
    }
  }
}