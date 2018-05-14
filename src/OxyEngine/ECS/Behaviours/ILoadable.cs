namespace OxyEngine.Ecs.Behaviours
{
  /// <summary>
  ///   Behaviour interface for every object that can be loaded
  /// </summary>
  public interface ILoadable
  {
    /// <summary>
    ///   Loading handler for IDrawable
    /// </summary>
    void Load();
  }
}