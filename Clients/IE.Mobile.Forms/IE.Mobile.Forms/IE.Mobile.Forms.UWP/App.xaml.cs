namespace IE.Mobile.Forms.UWP
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Windows.ApplicationModel.Activation;
    using Caliburn.Micro;
    using IE.Utilities.Extensions.Services;
    using IE.Mobile.Forms.UWP.Services;

    public sealed partial class App
    {
        private WinRTContainer _container;

        public App()
        {
            InitializeComponent();
        }

        protected override void Configure()
        {
            _container = new WinRTContainer();
            _container.RegisterWinRTServices();
            _container.Singleton<IE.Mobile.Forms.App>();
            _container.PerRequest<IPlayAudio, AudioService>();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;

            Xamarin.Forms.Forms.Init(args);

            // loads our MainPage as the root frame
            DisplayRootView<HomePage>();
        }

        #region IoC Overrides
        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().GetTypeInfo().Assembly
            };
        }
        #endregion
    }
}