using IE.Utilities.Common;
using IE.Utilities.Extensions.Services;
using Plugin.SimpleAudioPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IE.Mobile.Forms.UWP.Services
{
    public class AudioService : IPlayAudio
    {
        public void PlayAudio(SoundEffectType effect)
        {
            string fileName = "";
            switch (effect)
            {
                case SoundEffectType.FacebookAlert:
                    fileName = "~Assets/facebook_sound.mp3";
                    break;
                case SoundEffectType.FacebookPop:
                    fileName = "~Assets/facebook_pop.mp3";
                    break;
                case SoundEffectType.Notification:
                    fileName = "";
                    break;
            }

            //Platform Specific UWP code for running audio 

            ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(File.OpenRead(fileName));
            player.Play();
        }

        public void StopAudio()
        {
            throw new NotImplementedException();
        }
    }
}
