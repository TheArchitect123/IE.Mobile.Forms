using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Caliburn.Micro;
using FFImageLoading;
using FFImageLoading.Forms.Platform;
using Foundation;
using ModernHttpClient;
using ObjCRuntime;
using Plugin.Connectivity;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfPullToRefresh.XForms.iOS;
using Syncfusion.XForms.iOS.Shimmer;
using Syncfusion.XForms.iOS.TabView;
using UIKit;
using UserNotifications;

namespace IE.Mobile.Forms.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private readonly CaliburnAppDelegate appDelegate = new CaliburnAppDelegate(); //Call Application Delegate in order to register a new platform services 

        private void ConfigureFFImageLoader()
        {
            ImageService.Instance.Initialize(new FFImageLoading.Config.Configuration()
            {
                HttpClient = new HttpClient(new NativeMessageHandler()
                {
                    PreAuthenticate = false,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    AllowAutoRedirect = false
                })
            });
        }

        private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
        }

        private void InitSyncfusionRenderers()
        {
            SfTabViewRenderer.Init(); //Initialize 
            new Syncfusion.SfNavigationDrawer.XForms.iOS.SfNavigationDrawerRenderer();
            SfListViewRenderer.Init();
            SfPullToRefreshRenderer.Init();
            new SfBusyIndicatorRenderer();
            SfShimmerRenderer.Init();
        }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            InitSyncfusionRenderers();

            Rg.Plugins.Popup.Popup.Init();
            XF.Material.iOS.Material.Init();
            CachedImageRenderer.InitImageSourceHandler();
            CachedImageRenderer.Init(); //Register for Cached Image Library
            Stormlion.PhotoBrowser.iOS.Platform.Init();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; //Manage any Unhandled Exceptions
            ConfigureNotificationHubs();
            ConfigureFFImageLoader();
            InitializeAnyNotifications();

            LoadApplication(IoC.Get<App>());
            return base.FinishedLaunching(app, options);
        }

        #region Exception Handling 
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {

        }
        #endregion


        #region Local Notifications -- Device Orientations
        
        private void InitializeAnyNotifications()
        {
            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged; //Subscribe to any changes to Connectivity
        }

        public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations(UIApplication application, [Transient] UIWindow forWindow)
        {
            return UIInterfaceOrientationMask.Portrait;
        }

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {
            base.ReceivedLocalNotification(application, notification);

            //Output some text here
        }

        private void ConfigureNotificationHubs()
        {
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound,
                                                                    (granted, error) =>
                                                                    {
                                                                        if (granted)
                                                                            InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);
                                                                    });
        }
        #endregion
    }
}
