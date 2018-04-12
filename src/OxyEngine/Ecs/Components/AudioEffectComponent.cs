using Microsoft.Xna.Framework.Audio;

namespace OxyEngine.Ecs.Components
{
  public class AudioEffectComponent : GameComponent
  {
    public SoundEffect AudioEffect { get; set; }

    public void FireAndForget()
    {
      AudioEffect.Play();
    }
  }
}