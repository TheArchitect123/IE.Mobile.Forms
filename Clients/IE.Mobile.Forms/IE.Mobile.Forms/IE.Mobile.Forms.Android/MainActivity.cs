
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Caliburn.Micro;
using Plugin.Connectivity.Abstractions;
using FFImageLoading;
using ModernHttpClient;
using System.Net.Http;
using System.Net;
using Plugin.Media;
using Plugin.CurrentActivity;
using FFImageLoading.Forms.Platform;
using Android.Util;
using Plugin.Connectivity;
using System;
using PanCardView.Droid;

namespace IE.Mobile.Forms.Droid
{
    [Activity(Label = "packman", Icon = "@drawable/appicon", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState); //Register the Xamarin Forms Framework first
            base.OnCreate(savedInstanceState);

            ConfigureFFImageLoader();
            InitializeServices(savedInstanceState);
            LoadApplication(IoC.Get<App>());
        }

        #region Initialize Cache Frameworks
        private void ConfigureFFImageLoader()
        {
            ImageService.Instance.Initialize(new FFImageLoading.Config.Configuration()
            {
                HttpClient = new HttpClient(new NativeMessageHandler()
                {
                    PreAuthenticate = false,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate, //Enable Both Gzip Or Defalte Compression Algorithms
                    AllowAutoRedirect = false
                })
            });
        }
        #endregion

        /// <summary>
        /// Initialize any Services 
        /// </summary>
        /// <param name="bundle"></param>
        private void InitializeServices(Bundle bundle)
        {
            CrossCurrentActivity.Current.Init(this, bundle);
            CardsViewRenderer.Preserve();
            XF.Material.Droid.Material.Init(CrossCurrentActivity.Current.Activity, bundle); //Material Design Library
            CrossMedia.Current.Initialize();

            Rg.Plugins.Popup.Popup.Init(CrossCurrentActivity.Current.Activity, bundle);
            CachedImageRenderer.InitImageViewHandler();
            CachedImageRenderer.Init(true);
            Stormlion.PhotoBrowser.Droid.Platform.Init(CrossCurrentActivity.Current.Activity);

            ConfigureFFImageLoader();
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged; ; //Subscribe to any changes to Connectivity
        }

        private void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.IsConnected)
                Console.WriteLine("Specify any logging here");  //Logs are all written into a sqlite file
        }

        protected override void OnRestoreInstanceState(Bundle savedInstanceState)
        {
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}