using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using OxyEngine.Interfaces;

namespace OxyEngine.Audio
{
  public class AudioManager : IModule
  {
    public SoundEffectInstance NewEffectInstance(SoundEffect effect)
    {
      return effect.CreateInstance();
    }

    public void SetSongRepeating(bool isRepeating)
    {
      MediaPlayer.IsRepeating = isRepeating;
    }
    
    public void PlaySong(Song song)
    {
      MediaPlayer.Play(song);
    } 
    
    public void PauseSong()
    {
      MediaPlayer.Pause();
    } 
    
    public void ResumeSong()
    {
      MediaPlayer.Resume();
    } 
    
    public void StopSong()
    {
      MediaPlayer.Stop();
    }

    public void PlayEffect(SoundEffect effect)
    {
      effect.Play();
    }

    public void PlayEffectInstance(SoundEffectInstance effectInstance, bool isLooping)
    {
      effectInstance.IsLooped = isLooping;
      effectInstance.Play();
    }
    
    public void PauseEffectInstance(SoundEffectInstance effectInstance)
    {
      effectInstance.Pause();
    }
    
    public void ResumeEffectInstance(SoundEffectInstance effectInstance)
    {
      effectInstance.Resume();
    }
    
    public void StopEffectInstance(SoundEffectInstance effectInstance)
    {
      effectInstance.Stop();
    }
  }
}