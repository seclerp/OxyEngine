using Microsoft.Xna.Framework.Media;
using OxyEngine.Audio;
using OxyEngine.Ecs.Behaviours;

namespace OxyEngine.Ecs.Components
{
  public class SongComponent : GameComponent, ILoadable
  {
    public Song Song { get; set; }
    public bool Loop { get; set; }
    
    private AudioManager _manager;
    
    public void Load()
    {
      _manager = GetApi().Audio;
    }

    public void Play()
    {
      _manager.PlaySong(Song);
    }
    
    public void Pause()
    {
      _manager.PauseSong();
    }
    
    public void Stop()
    {
      _manager.StopSong();
    }
  }
}