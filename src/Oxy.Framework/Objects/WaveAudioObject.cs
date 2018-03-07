using System;
using OpenTK.Audio.OpenAL;
using Oxy.Framework.Enums;

namespace Oxy.Framework.Objects
{
  public class WaveAudioObject : AudioObject, IDisposable
  {
    private readonly int _bufferId;
    private readonly int _sourceId;

    private bool _isLooping;
    private int _stateId;

    public WaveAudioObject(byte[] data, int channels, int bits, int rate, AudioFormat audioFormat) : base(data,
      channels, bits, rate, audioFormat)
    {
      _bufferId = AL.GenBuffer();
      _sourceId = AL.GenSource();

      AL.BufferData(_bufferId, GetALSoundFormat(_channels, _bits), _data, _data.Length, _rate);
      AL.Source(_sourceId, ALSourcei.Buffer, _bufferId);
    }

    public void Dispose()
    {
      AL.SourceStop(_sourceId);
      AL.DeleteSource(_sourceId);
      AL.DeleteBuffer(_bufferId);
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

    public override void Stop()
    {
      AL.SourceStop(_sourceId);
    }

    public override void SetLoop(bool loop = true)
    {
      _isLooping = loop;
      AL.Source(_sourceId, ALSourceb.Looping, _isLooping);
    }

    public override bool GetLoop()
    {
      return _isLooping;
    }
  }
}