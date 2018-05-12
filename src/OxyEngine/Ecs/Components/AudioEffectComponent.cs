using Microsoft.Xna.Framework.Audio;
using OxyEngine.Audio;
using OxyEngine.Ecs.Behaviours;

namespace OxyEngine.Ecs.Components
{
  /// <summary>
  ///   Component for playing short audio effetcs
  /// </summary>
  public class AudioEffectComponent : GameComponent, ILoadable
  {
    /// <summary>
    ///   Effect to play
    /// </summary>
    public SoundEffect AudioEffect { get; set; }
    
    /// <summary>
    ///   Play mode
    /// </summary>
    public AudioEffectPlayMode PlayMode { get; set; }
    
    /// <summary>
    ///   Is effect must be looped
    /// </summary>
    public bool Loop { get; set; }

    private SoundEffect _lastEffect;
    private SoundEffectInstance _instance;
    private AudioManager _manager;
    
    public void Load()
    {
      _manager = GetApiManager().Audio;
    }
    
    /// <summary>
    ///   Start playing of effect
    /// </summary>
    public void Play()
    {
      switch (PlayMode)
      {
        case AudioEffectPlayMode.FireAndForget:
          AudioEffect.Play();
          break;
        
        case AudioEffectPlayMode.Synchronous:
          CheckChanges();
          _instance.Play();
          _instance.IsLooped = Loop;
          break;
      }
    }

    /// <summary>
    ///   Pause playing of effect
    ///   Works only for Synchronous
    /// </summary>
    public void Pause()
    {
      switch (PlayMode)
      {
        case AudioEffectPlayMode.Synchronous:
          _instance?.Pause();
          break;
      }
    }

    /// <summary>
    ///   Stop playing of effect
    ///   Works only for Synchronous
    /// </summary>
    public void Stop()
    {
      switch (PlayMode)
      {
        case AudioEffectPlayMode.Synchronous:
          _instance?.Stop();
          break;
      }
    }

    private void CheckChanges()
    {
      if (_instance == null || _lastEffect != AudioEffect)
      {
        _instance = _manager.NewEffectInstance(AudioEffect);
        _lastEffect = AudioEffect;
      }
    }
  }
}