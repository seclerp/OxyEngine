namespace Oxy.Framework.Objects
{
  public abstract class AudioObject
  {
    protected readonly byte[] _data;
    protected readonly AudioFormat _format;
    
    public int Channels { get; }
    public int Bits { get; }
    public int Rate { get; }
    public string Format { get; }

    protected AudioObject(byte[] data, int channels, int bits, int rate, AudioFormat format)
    {
      _data = data;
      Channels = channels;
      Bits = bits;
      Rate = rate;
      _format = format;
      Format = format.ToString().ToLower();
    }

    protected abstract AudioState GetCurrentState();
    public abstract void Play();
    public abstract void Pause();
    public abstract void Stop();
    public abstract void SetLoop(bool loop = true);
    public abstract bool GetLoop();
    public string GetState() => 
      GetCurrentState().ToString().ToLower();
  }
}