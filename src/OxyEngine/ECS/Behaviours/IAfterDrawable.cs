namespace OxyEngine.Ecs.Behaviours
{
  /// <summary>
  ///   Behaviour interface for every object that can be called after draw
  /// </summary>
  public interface IAfterDrawable
  {
    /// <summary>
    ///   AfterDraw handler for IAfterDrawable
    /// </summary>
    void AfterDraw();
  }
}