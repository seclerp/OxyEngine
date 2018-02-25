namespace Oxy.Framework.Objects
{
  /// <summary>
  /// Object for playing audio
  /// </summary>
  public abstract class AudioObject
  {
    protected readonly byte[] _data;
    protected readonly AudioFormat _audioFormat;

    protected int _channels;
    protected int _bits;
    protected int _rate;
    protected string _format;

    protected AudioObject(byte[] data, int channels, int bits, int rate, AudioFormat audioFormat)
    {
      _data = data;
      _channels = channels;
      _bits = bits;
      _rate = rate;
      _audioFormat = audioFormat;
      _format = audioFormat.ToString().ToLower();
    }

    protected abstract AudioState GetCurrentState();
    
    /// <summary>
    /// Play audio. Take no effect if already in Playing state
    /// </summary>
    public abstract void Play();
    
    /// <summary>
    /// Pause audio. Take no effect if not in Playing state. 
    /// </summary>
    public abstract void Pause();
    
    /// <summary>
    /// Stop audio. Take no effect if not in Playing state. 
    /// </summary>
    public abstract void Stop();
    
    /// <summary>
    /// Set looping
    /// </summary>
    public abstract void SetLoop(bool loop = true);
    
    /// <summary>
    /// Returns true if audio is looping, otherwise false
    /// </summary>
    /// <returns>true if audio is looping, otherwise false</returns>
    public abstract bool GetLoop();
    
    /// <summary>
    /// Returns current audio state
    /// </summary>
    /// <returns>"playing", "paused" or "stopped"</returns>
    public string GetState() => 
      GetCurrentState().ToString().ToLower();

    /// <summary>
    /// Returns audio channels count
    /// </summary>
    /// <returns>Audio channels count</returns>
    public int GetChannels() => _channels;
    public int GetBits() => _bits;
    public int GetRate() => _rate;
  }
}