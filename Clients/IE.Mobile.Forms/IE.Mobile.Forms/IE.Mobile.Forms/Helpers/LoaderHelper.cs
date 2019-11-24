using Caliburn.Micro;
using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Forms;
using IE.Utilities.Constants;


public static class LoaderHelper
{
    public static async Task<IMaterialModalPage> ShowAlertDialogueAsync(string title)
    {
        return await MaterialDialog.Instance.LoadingDialogAsync(title, new XF.Material.Forms.UI.Dialogs.Configurations.MaterialLoadingDialogConfiguration()
        {
            BackgroundColor = Color.White,
            MessageTextColor = Color.Black,
            ScrimColor = Color.Black,
            TintColor = ColorCodes.ButtonTheme,
            CornerRadius = 20
        });
    }
}