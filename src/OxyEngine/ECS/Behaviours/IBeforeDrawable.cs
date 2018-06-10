namespace OxyEngine.Ecs.Behaviours
{
  /// <summary>
  ///   Behaviour interface for every object that can be called before draw
  /// </summary>
  public interface IBeforeDrawable
  {
    /// <summary>
    ///   BeforeDraw handler for IBeforeDrawable
    /// </summary>
    void BeforeDraw();
  }
}