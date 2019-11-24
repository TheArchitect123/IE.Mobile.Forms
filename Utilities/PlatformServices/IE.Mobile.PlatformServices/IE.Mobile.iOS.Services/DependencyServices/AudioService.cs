using System;
using AVFoundation;
using System.IO;
using Foundation;
using IE.Utilities.Extensions.Services;
using IE.Utilities.Common;

namespace IE.Mobile.iOS.Services
{
    public class AudioService : IPlayAudio
    {
        public void PlayAudio(SoundEffectType effect)
        {
            string fileName = "";
            switch (effect)
            {
                case SoundEffectType.FacebookAlert:
                    fileName = "facebook_sound.mp3";
                    break;
                case SoundEffectType.FacebookPop:
                    fileName = "facebook_pop.mp3";
                    break;
                case SoundEffectType.Notification:
                    fileName = "";
                    break;
            }

            var filePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName),

                                                                 Path.GetExtension(fileName));

            NSError outError;
            NSUrl fileURL = new NSUrl(fileName);
            using (AVAudioPlayer audioPlayer = new AVAudioPlayer(url: fileURL, "video/mp4", outError: out outError))
            {
                var url = NSUrl.FromString(filePath);
                audioPlayer.Play();
            }
        }

        public void StopAudio()
        {
            throw new NotImplementedException();
        }
    }
}