using System;
using Android.App;
using Android.Runtime;
using Caliburn.Micro;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using System.Collections.Generic;
using System.Reflection;
using IE.MobileStorage.Sqlite;
using IE.Utilities.Extensions;
using IE.Utilities.Extensions.Services;
using IE.Mobile.Droid.Services;

namespace IE.Mobile.Forms.Droid
{
    /// <summary>
    /// Register this class to the manifest as the main application manager (This is done automatically via reflection)
    /// </summary>
    [Application(Debuggable = true)]
    public class MainApplication : CaliburnApplication
    {
        private SimpleContainer _container;

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        protected override void Configure()
        {
            _container = new SimpleContainer();

            // make the container available for resolution
            _container.Instance(_container);
            _container.Singleton<App>(); //Register the application PCL dependency first then register all other platform specific dependencies
            _container.PerRequest<IPlayAudio, AudioService>();

            //Local DB Mngr
          //  string DbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IEModule.db");
        //    DbPath.CreateFileIfNotExists(); //Create the File from the Utilities library

       //     SQLiteConnection DbConnection = new SQLiteConnection(new SQLitePlatformAndroid(), DbPath, false);
       //     _container.Instance<IDatabase>(new Database(DbPath, DbConnection)); //Register the Sqlite Apis -- This is found in the infrastructure directory

            // TODO: Register any Platform-Specific abstractions here

        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            // Get a list of all assemblies that will be used by the IoC container
            return new[]
            {
                GetType().Assembly,
                typeof(App).Assembly
            };
        }

        #region Caliburn IoC Methods
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
        #endregion
    }
}