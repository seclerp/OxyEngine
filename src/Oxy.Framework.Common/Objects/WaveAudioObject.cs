using System;
using Oxy.Framework.AL;

namespace Oxy.Framework.Objects
{
  public class WaveAudioObject : AudioObject, IDisposable
  {
    private readonly int _bufferId;

    private bool _isLooping;
    private readonly int _sourceId;
    private int _stateId;

    public WaveAudioObject(byte[] data, int channels, int bits, int rate, AudioFormat audioFormat) : base(data,
      channels, bits, rate, audioFormat)
    {
      _bufferId = AL.AL.GenBuffer();
      _sourceId = AL.AL.GenSource();

      AL.AL.BufferData(_bufferId, GetALSoundFormat(_channels, _bits), _data, _data.Length, _rate);
      AL.AL.Source(_sourceId, ALSourcei.Buffer, _bufferId);
    }

    public void Dispose()
    {
      AL.AL.SourceStop(_sourceId);
      AL.AL.DeleteSource(_sourceId);
      AL.AL.DeleteBuffer(_bufferId);
    }

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
      switch (AL.AL.GetSourceState(_sourceId))
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

    public override void Play()
    {
      if (GetCurrentState() == AudioState.Playing)
        return;

      AL.AL.SourcePlay(_sourceId);
    }

    public override void Pause()
    {
      if (GetCurrentState() != AudioState.Playing)
        return;

      AL.AL.SourcePause(_sourceId);
    }

    public override void Stop()
    {
      AL.AL.SourceStop(_sourceId);
    }

    public override void SetLoop(bool loop = true)
    {
      _isLooping = loop;
      AL.AL.Source(_sourceId, ALSourceb.Looping, _isLooping);
    }

    public override bool GetLoop()
    {
      return _isLooping;
    }
  }
}