using System.CodeDom;
using Microsoft.Xna.Framework.Audio;
using OxyEngine.Audio;
using OxyEngine.Ecs.Behaviours;

namespace OxyEngine.Ecs.Components
{
  public class AudioEffectComponent : GameComponent, ILoadable
  {
    public SoundEffect AudioEffect { get; set; }
    public AudioEffectPlayMode PlayMode { get; set; }
    public bool Loop { get; set; }

    private SoundEffect _lastEffect;
    private SoundEffectInstance _instance;
    private AudioManager _manager;
    
    public void Load()
    {
      _manager = GetApi().Audio;
    }
    
    public void Play()
    {
      switch (PlayMode)
      {
        case AudioEffectPlayMode.FireAndForget:
          AudioEffect.Play();
          break;
        
        case AudioEffectPlayMode.Syncronious:
          CheckChanges();
          _instance.Play();
          _instance.IsLooped = Loop;
          break;
      }
    }

    public void Pause()
    {
      switch (PlayMode)
      {
        case AudioEffectPlayMode.Syncronious:
          _instance?.Pause();
          break;
      }
    }

    public void Stop()
    {
      switch (PlayMode)
      {
        case AudioEffectPlayMode.Syncronious:
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