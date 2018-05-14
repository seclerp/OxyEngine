namespace OxyEngine.Ecs.Behaviours
{
  /// <summary>
  ///   Behaviour interface for every object that can be initialized
  /// </summary>
  public interface IInitializable
  {
    /// <summary>
    ///   Initialization handler for IDrawable
    /// </summary>
    void Init();
  }
}