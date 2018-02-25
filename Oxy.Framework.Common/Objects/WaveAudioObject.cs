using System;
using OpenAL.NETCore.AL;

namespace Oxy.Framework.Objects
{
  public class WaveAudioObject : AudioObject, IDisposable
  {
    private int _bufferId;
    private int _sourceId;
    private int _stateId;

    private bool _isLooping;
    
    private static ALFormat GetALSoundFormat(int channels, int bits)
    {
      switch (channels)
      {
        case 1: return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
        case 2: return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
        default: throw new NotSupportedException("The specified sound format is not supported.");
      }
    }

    protected override AudioState GetCurrentState()
    {
      switch (AL.GetSourceState(_sourceId))
      {
        case ALSourceState.Initial:
        case ALSourceState.Stopped:
          return AudioState.Stopped;
        case ALSourceState.Playing:
          return AudioState.Playing;
        case ALSourceState.Paused:
          return AudioState.Paused;
      }
      
      throw new NotSupportedException();
    }
    
    public WaveAudioObject(byte[] data, int channels, int bits, int rate, AudioFormat format) : base(data, channels, bits, rate, format)
    {
      _bufferId = AL.GenBuffer();
      _sourceId = AL.GenSource();
      
      AL.BufferData(_bufferId, GetALSoundFormat(Channels, Bits), _data, _data.Length, Rate);
      AL.Source(_sourceId, ALSourcei.Buffer, _bufferId);
    }

    public override void Play()
    {
      if (GetCurrentState() == AudioState.Playing)
        return;
      
      AL.SourcePlay(_sourceId);
    }

    public override void Pause()
    {
      if (GetCurrentState() != AudioState.Playing)
        return;
      
      AL.SourcePause(_sourceId);
    }

    public override void Stop() =>
      AL.SourceStop(_sourceId);

    public override void SetLoop(bool loop = true)
    {
      _isLooping = loop;
      AL.Source(_sourceId, ALSourceb.Looping, _isLooping);
    }

    public override bool GetLoop() =>
      _isLooping;

    public void Dispose()
    {
      AL.SourceStop(_sourceId);
      AL.DeleteSource(_sourceId);
      AL.DeleteBuffer(_bufferId);
    }
  }
}