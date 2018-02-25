using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using Oxy.Framework.Objects;

namespace Oxy.Framework
{
  /// <summary>
  /// Class for managing game assets
  /// </summary>
  public class Resources : LazyModule<Resources>
  {
    /// <summary>
    /// Loads font from path in library
    /// </summary>
    /// <param name="path">Path in library to font file</param>
    /// <param name="size">Size of the font</param>
    /// <returns>Font object</returns>
    /// <exception cref="FileNotFoundException">Fires when font cannot be found or file do not exists</exception>
    public static FontObject LoadFont(string path, float size = 12)
    {
      var fullPath = Path.Combine(Common.GetLibraryRoot(), path);
      
      if (!File.Exists(fullPath))
        throw new FileNotFoundException(path);
      
      var tempCollection = new PrivateFontCollection();
      
      tempCollection.AddFontFile(fullPath);
      
      var font = new Font(tempCollection.Families[0], size);
      
      return new FontObject(font);
    }

    public static AudioObject LoadAudio(string path)
    {
      var fullPath = Path.Combine(Common.GetLibraryRoot(), path);
      
      if (!File.Exists(fullPath))
        throw new FileNotFoundException(path);

      Func<BinaryReader, AudioObject> importer = null;
      AudioObject result;
      
      using (var stream = File.Open(fullPath, FileMode.Open))
      using (var reader = new BinaryReader(stream))
      {
        // Header
        var signature = new string(reader.ReadChars(4));
        
        // Signature shows that this is file with riff header
        // trying to read it as a wave
        if (signature == "RIFF")
        {
          importer = LoadWave;
        }
        // else if ... - there will be other formats strategy

        if (importer == null)
          throw new NotSupportedException($"Unknown or unsupported audio file: '{path}'");
        
        AudioObject audioObject = importer.Invoke(reader);
        
        if (audioObject == null)
          throw new NotSupportedException($"Unable to load audio file: '{path}'");

        return audioObject;
      }
    }

    private static WaveAudioObject LoadWave(BinaryReader reader)
    {
      var riffChunckSize = reader.ReadInt32();
      
      var format = new string(reader.ReadChars(4));
      if (format != "WAVE")
        return null;

      var formatSignature = new string(reader.ReadChars(4));
      if (formatSignature != "fmt ")
        return null;
      
      int formatChunkSize = reader.ReadInt32();
      int audioFormat = reader.ReadInt16();
      int numChannels = reader.ReadInt16();
      int sampleRate = reader.ReadInt32();
      int byteRate = reader.ReadInt32();
      int blockAlign = reader.ReadInt16();
      int bitsPerSample = reader.ReadInt16();
      
      var dataSignature = new string(reader.ReadChars(4));
      if (dataSignature != "data")
        return null;

      int dataChunkSize = reader.ReadInt32();
      
      byte[] data = reader.ReadBytes((int)reader.BaseStream.Length);

      AudioFormat aFormat = GetSoundFormat(numChannels, bitsPerSample);
      
      return new WaveAudioObject(data, numChannels, bitsPerSample, sampleRate, aFormat);
    }

    private static AudioFormat GetSoundFormat(int channels, int bits)
    {
      switch (channels)
      {
        case 1: return bits == 8 ? AudioFormat.Mono8 : AudioFormat.Mono16;
        case 2: return bits == 8 ? AudioFormat.Stereo8 : AudioFormat.Stereo16;
        default: throw new NotSupportedException("The specified sound format is not supported.");
      }
    }
  }
  
}