using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Forms;
using IE.Utilities.Constants;

public static class AlertHelper
{
    public static async Task ShowAlertDialogueAsync(string message, string title, string confirmed = "Ok")
    {
        if (Device.RuntimePlatform != Device.UWP)
        {
            await AudioHelper.PlayAudioFacebookAlertSound().ConfigureAwait(false);
            await MaterialDialog.Instance.AlertAsync(message, title, confirmed, new XF.Material.Forms.UI.Dialogs.Configurations.MaterialAlertDialogConfiguration()
            {
                BackgroundColor = Color.White,
                MessageTextColor = Color.Black,
                ScrimColor = Color.Black,
                TintColor = ColorCodes.ButtonTheme,
                CornerRadius = 20
            });
        }
        else
            await Application.Current.MainPage.DisplayAlert(title, message, confirmed, "Cancel");
    }

    public static async Task<bool?> ShowConfirmationDialogueAsync(string message, string title, string confirmed = "Ok", string cancel = "Cancel")
    {
        if (Device.RuntimePlatform != Device.UWP)
        {
            await AudioHelper.PlayAudioFacebookAlertSound().ConfigureAwait(false);
            var stdResponse = await MaterialDialog.Instance.ConfirmAsync(message, title, confirmed, cancel, new XF.Material.Forms.UI.Dialogs.Configurations.MaterialAlertDialogConfiguration()
            {
                BackgroundColor = Color.White,
                MessageTextColor = Color.Black,
                ScrimColor = Color.Black,
                TintColor = ColorCodes.ButtonTheme,
                CornerRadius = 20
            });

            if (stdResponse.HasValue && stdResponse.Value)
                return true;
            else
                return false;
        }
        else
            return await Application.Current.MainPage.DisplayAlert(title, message, confirmed, cancel);
    }
}
