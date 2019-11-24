using System.IO;
using Xamarin.Forms;
using Caliburn.Micro;
using System.Threading.Tasks;
using System;
using IE.Utilities.Extensions.Services;
using IE.Utilities.Common;

public static class AudioHelper
{
    public static async Task PlayAudioTwitterSound()
    {
        IoC.Get<IPlayAudio>().PlayAudio(SoundEffectType.FacebookPop);
    }

    public static async Task PlayAudioFacebookAlertSound()
    {
        IoC.Get<IPlayAudio>().PlayAudio(SoundEffectType.FacebookAlert);
    }
}