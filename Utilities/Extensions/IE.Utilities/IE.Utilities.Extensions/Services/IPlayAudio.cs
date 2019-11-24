namespace IE.Utilities.Extensions.Services
{
    using IE.Utilities.Common;
    public interface IPlayAudio
    {
        void PlayAudio(SoundEffectType effect);
        void StopAudio();
    }
}
