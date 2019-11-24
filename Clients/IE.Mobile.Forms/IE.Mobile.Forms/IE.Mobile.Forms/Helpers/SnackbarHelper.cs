using System.Threading.Tasks;
using XF.Material.Forms.UI.Dialogs;
using Xamarin.Forms;
using IE.Utilities.Constants;
using NM.Utilities.Constants;

public static class SnackbarHelper
{
    public static async Task ShowStndrdSnackbarAsync(string message)
    {
        await MaterialDialog.Instance.SnackbarAsync(message, "Got it", 2000,
                      new XF.Material.Forms.UI.Dialogs.Configurations.MaterialSnackbarConfiguration()
                      {
                          BackgroundColor = Color.White,
                          MessageTextColor = Color.Black,
                          ScrimColor = Color.Black,
                          TintColor = ColorCodes.ButtonTheme
                      });
    }
}
