using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Net;
using IronPython.Modules;
using Oxy.Framework.Enums;
using Oxy.Framework.Objects;

namespace Oxy.Framework
{
  /// <summary>
  ///   Class for managing game assets
  /// </summary>
  public class Resources : LazyModule<Resources>
  {
    /// <summary>
    ///   Loads texture from path in library
    /// </summary>
    /// <param name="path">Path in library to texture file</param>
    /// <returns>Texture object</returns>
    /// <exception cref="FileNotFoundException">Fires when texture cannot be found or file does not exist</exception>
    public static TextureObject LoadTexture(string path)
    {
      var fullPath = Path.Combine(Common.GetAssetsRoot(), path);

      if (!File.Exists(fullPath))
        throw new FileNotFoundException(path);

      var texture = new Bitmap(Image.FromFile(fullPath));

      return new TextureObject(texture);
    }

    /// <summary>
    ///   Loads texture from stream
    /// </summary>
    /// <param name="stream">Stream to read</param>
    /// <returns>Texture object</returns>
    /// <exception cref="NullReferenceException">If stream is null</exception>
    public static TextureObject LoadTexture(Stream stream)
    {
      if (stream == null)
        throw new NullReferenceException(nameof(stream));

      var texture = new Bitmap(stream);

      return new TextureObject(texture);
    }

    /// <summary>
    ///   Loads font from path in library
    /// </summary>
    /// <param name="path">Path in library to font file</param>
    /// <param name="size">Size of the font</param>
    /// <returns>Font object</returns>
    /// <exception cref="FileNotFoundException">Fires when font cannot be found or file do not exists</exception>
    public static TtfFontObject LoadFont(string path, float size = 12)
    {
      var fullPath = Path.Combine(Common.GetAssetsRoot(), path);

      if (!File.Exists(fullPath))
        throw new FileNotFoundException(path);

      var tempCollection = new PrivateFontCollection();

      tempCollection.AddFontFile(fullPath);

      var font = new Font(tempCollection.Families[0], size);

      return new TtfFontObject(font);
    }

    /// <summary>
    ///   Loads font from stream
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="size"></param>
    /// <returns>Font object</returns>
    /// <exception cref="NullReferenceException">If stream is null</exception>
    public static TtfFontObject LoadFont(Stream stream, float size = 12)
    {
      if (stream == null)
        throw new NullReferenceException(nameof(stream));

      var tempCollection = new PrivateFontCollection();

      byte[] fontdata = new byte[stream.Length];
      stream.Read(fontdata, 0, (int) stream.Length);
      stream.Close();
      unsafe
      {
        fixed (byte* pFontData = fontdata)
        {
          tempCollection.AddMemoryFont((IntPtr) pFontData, fontdata.Length);
        }
      }

      var font = new Font(tempCollection.Families[0], size);

      return new TtfFontObject(font);
    }

    /// <summary>
    ///   Loads bitmap font from file
    /// </summary>
    /// <param name="path"></param>
    /// <param name="characters"></param>
    /// <returns></returns>
    public static BitmapFontObject LoadBitmapFont(string path, string characters)
    {
      return new BitmapFontObject(LoadTexture(path), characters, true);
    }
    
    /// <summary>
    ///   Loads bitmap font from texture
    /// </summary>
    /// <param name="bitmap"></param>
    /// <param name="characters"></param>
    /// <returns></returns>
    public static BitmapFontObject LoadBitmapFont(TextureObject bitmap, string characters)
    {
      return new BitmapFontObject(bitmap, characters);
    }

    /// <summary>
    ///   Loads audio from path in library
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public static AudioObject LoadAudio(string path)
    {
      var fullPath = Path.Combine(Common.GetAssetsRoot(), path);

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
        if (signature == "RIFF") importer = LoadWave;
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

      byte[] data = reader.ReadBytes((int) reader.BaseStream.Length);

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