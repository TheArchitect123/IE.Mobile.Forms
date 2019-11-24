using Caliburn.Micro;
using Caliburn.Micro.Xamarin.Forms;
using IE.Mobile.Forms.Services;
using IE.Mobile.Forms.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.Resources;
using XF.Material.Forms.UI.Dialogs;
using XF.Material.Forms.UI.Dialogs.Configurations;

namespace IE.Mobile.Forms
{
    public partial class App : FormsApplication
    {
        private SimpleContainer _container;
        public App(SimpleContainer container)
        {
            InitializeComponent();
            Initialize();

            this._container = container;


            // TODO: Register additional viewmodels and services
            _container
                .PerRequest<BaseViewModel>()
                .PerRequest<PackmanPageViewModel>()
                .PerRequest<PackmanScorePageViewModel>()
                .PerRequest<PackmanStatusPageViewModel>();

            //Azure Infrastructure
            //    .PerRequest<BlobStorageService>()

            // register services
            if (Device.RuntimePlatform != Device.UWP)
                _container.Singleton<IEventAggregator, EventAggregator>(); //Better than Messaging Center

            // setup root page as a navigation page
            InitializeServices();
            //   ConfigureStylesheets(); -- Override the Dialog Stylesheets

            LoadRootPage();
        }

        private void ConfigureStylesheets()
        {
            var alertDialogConfiguration = new MaterialAlertDialogConfiguration
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
                TitleTextColor = Color.White,
                TitleFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.RobotoRegular"),
                MessageTextColor = Color.White,
                MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.RobotoRegular"),
                TintColor = Color.White,
                ButtonFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.RobotoRegular"),
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#011A27").MultiplyAlpha(0.32),
                ButtonAllCaps = false
            };

            var alertconfirmationDialogConfiguration = new MaterialConfirmationDialogConfiguration
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
                TitleTextColor = Color.White,
                TitleFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.RobotoRegular"),
                TintColor = Color.White,
                ButtonFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.RobotoRegular"),
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#011A27").MultiplyAlpha(0.32),
                ButtonAllCaps = false
            };

            var alertloadingDialogConfiguration = new MaterialLoadingDialogConfiguration
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
                MessageTextColor = Color.White,
                TintColor = Color.White,
                MessageFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.RobotoRegular"),
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#011A27").MultiplyAlpha(0.32)
            };

            var alertsimpleDialogConfiguration = new MaterialSimpleDialogConfiguration
            {
                BackgroundColor = XF.Material.Forms.Material.GetResource<Color>(MaterialConstants.Color.PRIMARY),
                TitleTextColor = Color.White,
                TitleFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.RobotoRegular"),
                TintColor = Color.White,
                TextFontFamily = XF.Material.Forms.Material.GetResource<OnPlatform<string>>("FontFamily.RobotoRegular"),
                CornerRadius = 8,
                ScrimColor = Color.FromHex("#011A27").MultiplyAlpha(0.32),
                TextColor = Color.White
            };

            MaterialDialog.Instance.SetGlobalStyles(alertDialogConfiguration, alertloadingDialogConfiguration, null, alertsimpleDialogConfiguration, alertconfirmationDialogConfiguration);
        }

        private void LoadRootPage()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViews = "IE.Mobile.Forms.Views",
                DefaultSubNamespaceForViewModels = "IE.Mobile.Forms.ViewModels"
            };
            Caliburn.Micro.Xamarin.Forms.ViewLocator.ConfigureTypeMappings(config);
            Caliburn.Micro.Xamarin.Forms.ViewModelLocator.ConfigureTypeMappings(config);

            DisplayRootViewFor<PackmanPageViewModel>();
        }

        private void InitializeServices()
        {
            XF.Material.Forms.Material.Init(this, "Material.Configuration");
        }

        protected override void PrepareViewFirst(NavigationPage navigationPage)
        {
            _container.Instance<Caliburn.Micro.Xamarin.Forms.INavigationService>(new NavigationPageAdapter(navigationPage));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            // Force container to create instances of all IApplicationServices by calling toArray()
            var services = _container.GetAllInstances<IApplicationService>().ToArray();
            foreach (var service in services)
                service.Initialize();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
