using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using OxyEngine.Events;
using OxyEngine.Events.Args;
using OxyEngine.Interfaces;
using OxyEngine.Loggers;
using OxyEngine.Settings;

namespace OxyEngine.Resources
{
  /// <summary>
  ///   Module for managing game assets. Wrapper around using ContentManager
  /// </summary>
  public class ResourceManager : IModule, IDisposable
  {
    private ContentManager _manager;
    private IEnumerable<IDisposable> _loadedResources;
    // ReSharper disable once NotAccessedField.Local
    private ResourcesSettings _settings;
    
    #region Initialization

    /// <summary>
    ///   Initialize Resources module
    /// </summary>
    /// <param name="gameInstance">Game entry point</param>
    /// <param name="manager">MonoGame ContentManager to use</param>
    /// <param name="settings">Resources settings</param>
    public ResourceManager(GameInstance gameInstance, ContentManager manager, ResourcesSettings settings)
    {
      _settings = settings;
      _manager = manager ?? throw new NullReferenceException(nameof(manager));
      _loadedResources = new List<IDisposable>();
      
      gameInstance.Events.Global.AddListenersUsingAttributes(this);
    }

    #endregion
    
    /// <summary>
    ///   Free memory used by resources
    ///   Call this OnUnload
    /// </summary>
    public void Dispose()
    {
      foreach (var resource in _loadedResources)
      {
        // Because we can't assign to null iterator, we are using temp variable
        var t = resource;
        // resource can be null already, so check it before call to not catch NullReferenceException
        t?.Dispose();
      }
    }
    
    /// <summary>
    ///   Event listener for global unload event
    /// </summary>
    [ListenEvent(EventNames.Initialization.OnUnload)]
    private void OnUnload(object sender, EngineEventArgs args)
    {
      Dispose();
    }
    
    #region Public API

    /// <summary>
    ///   Loads texture from path in library
    /// </summary>
    /// <param name="path">Path in library to texture</param>
    /// <returns>Texture object</returns>
    /// <exception cref="FileNotFoundException">Fires when texture cannot be found or file does not exist</exception>
    public Texture2D LoadTexture(string path)
    {
      return LoadValidate<Texture2D>(path);
    }

    /// <summary>
    ///   Loads TrueType or Bitmap font from path in library
    /// </summary>
    /// <param name="path">Path in library to font</param>
    /// <returns>Font object</returns>
    /// <exception cref="FileNotFoundException">Fires when font cannot be found or file do not exists</exception>
    public SpriteFont LoadFont(string path)
    {
      return LoadValidate<SpriteFont>(path);
    }

    /// <summary>
    ///   Loads sound from path in library
    /// </summary>
    /// <param name="path">Path in library to sound effect</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public SoundEffect LoadSoundEffect(string path)
    {
      return LoadValidate<SoundEffect>(path);
    }
    
    /// <summary>
    ///   Loads song from path in library
    /// </summary>
    /// <param name="path">Path in library to song</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="NotSupportedException"></exception>
    public Song LoadSong(string path)
    {
      return LoadValidate<Song>(path);
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

      LogManager.Log($"Resource loaded: '{path}'");
      
      return result;
    }

    #endregion
  }
}