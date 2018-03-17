using System;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Oxy.Framework.Settings;

namespace Oxy.Framework
{
  /// <summary>
  ///   Module for managing game assets. Wrapper around using ContentManager
  /// </summary>
  public class Resources : IModule
  {
    private ContentManager _manager;

    #region Initialization

    /// <summary>
    ///   Initialize Resources module
    /// </summary>
    /// <param name="manager">MonoGame ContentManager to use</param>
    public Resources(ContentManager manager, ResourcesSettings settings)
    {
      _manager = manager ?? throw new NullReferenceException(nameof(manager));
    }

    #endregion

    #region Public API

    /// <summary>
    ///   Loads texture from path in library
    /// </summary>
    /// <param name="path">Path in library to texture file</param>
    /// <returns>Texture object</returns>
    /// <exception cref="FileNotFoundException">Fires when texture cannot be found or file does not exist</exception>
    public Texture2D LoadTexture(string path)
    {
      return LoadValidate<Texture2D>(path);
    }

    /// <summary>
    ///   Loads TrueType or Bitmap font from path in library
    /// </summary>
    /// <param name="path">Path in library to font file</param>
    /// <param name="size">Size of the font</param>
    /// <returns>Font object</returns>
    /// <exception cref="FileNotFoundException">Fires when font cannot be found or file do not exists</exception>
    public SpriteFont LoadFont(string path)
    {
      return LoadValidate<SpriteFont>(path);
    }

    /// <summary>
    ///   Loads sound from path in library
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public SoundEffect LoadSound(string path)
    {
      return LoadValidate<SoundEffect>(path);
    }

    #endregion

    #region Private members

    private T LoadValidate<T>(string path)
    {
      if (path == null)
        throw new NullReferenceException(nameof(path));

      var result = _manager.Load<T>(path);
      
      if (result == null)
        throw new Exception($"Asset '{path}' not found or can't be loaded");

      return result;
    }

    #endregion
  }
}