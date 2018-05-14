using Microsoft.Xna.Framework.Media;
using OxyEngine.Audio;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;

namespace OxyEngine.Ecs.Components
{
  /// <summary>
  ///   Component for playing songs (ambient, long audio files)
  /// </summary>
  public class SongComponent : GameComponent, ILoadable
  {
    /// <summary>
    ///   Song to play
    /// </summary>
    public Song Song { get; set; }
    
    /// <summary>
    ///   If song must be looped
    /// </summary>
    public bool Loop { get; set; }
    
    private AudioManager _audioManager;
    
    public void Load()
    {
      _audioManager = Container.Instance.ResolveByName<AudioManager>(InstanceName.AudioManager);
    }

    /// <summary>
    ///   Start playing of a song
    /// </summary>
    public void Play()
    {
      _audioManager.PlaySong(Song);
    }
    
    /// <summary>
    ///   Pause song
    /// </summary>
    public void Pause()
    {
      _audioManager.PauseSong();
    }
    
    /// <summary>
    ///   Stop song
    /// </summary>
    public void Stop()
    {
      _audioManager.StopSong();
    }
  }
}