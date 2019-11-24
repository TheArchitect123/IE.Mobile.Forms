using Android.Media;
using Android.Net;
using IE.Utilities.Extensions.Services;
using IE.Utilities.Common;

namespace IE.Mobile.Droid.Services
{
    public class AudioService : IPlayAudio
    {
        public void PlayAudio(SoundEffectType effect)
        {
            int? soundId = null;
            switch (effect)
            {
                case SoundEffectType.FacebookAlert:
                    soundId = IE.Mobile.Droid.Services.Resource.Raw.facebook_sound;
                    break;
                case SoundEffectType.FacebookPop:
                    soundId = IE.Mobile.Droid.Services.Resource.Raw.facebook_pop;
                    break;
                case SoundEffectType.Notification:
                    soundId = IE.Mobile.Droid.Services.Resource.Raw.facebook_pop;
                    break;
            }

            //Dispose of the media Player on each call to avoid any memory leaks
            using (MediaPlayer _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, soundId.Value))
            {
                _mediaPlayer.SetVolume(100, 100);
                _mediaPlayer.Start();
            }
        }

        public void StopAudio()
        {
            throw new System.NotImplementedException();
        }
    }
}