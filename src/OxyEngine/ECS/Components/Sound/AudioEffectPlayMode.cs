namespace OxyEngine.Ecs.Components.Sound
{
  /// <summary>
  ///   Audio effect play mode
  ///   Represent flow of playing effect.
  ///     FireAndForget - non-controlable,
  ///     Synchronous - can be paused, stopped or resumed
  /// </summary>
  public enum AudioEffectPlayMode
  {
    FireAndForget,
    Synchronous
  }
}