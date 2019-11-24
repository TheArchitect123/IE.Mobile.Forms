namespace NM.Utilities.Constants
{
    using Plugin.Connectivity;
    using Xamarin.Forms;

    public static class ApplicationConstants
    {
        public static double ScreenWidth = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width;
        public static double ScreenHeight = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height;
        public static Point Origin = new Point(0, ScreenHeight);

        public static bool IsConnected => CrossConnectivity.Current.IsConnected;
    }
}