using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using IE.Mobile.iOS.Services;
using IE.MobileStorage.Sqlite;
using IE.Utilities.Extensions;
using IE.Utilities.Extensions.Services;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

namespace IE.Mobile.Forms.iOS
{
    class CaliburnAppDelegate : CaliburnApplicationDelegate
    {
        private SimpleContainer _container;

        public CaliburnAppDelegate()
        {
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
            string DbPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "IEModule.db");
            DbPath.CreateFileIfNotExists(); //Create the File from the Utilities library

            SQLiteConnection DbConnection = new SQLiteConnection(new SQLitePlatformIOS(), DbPath, false);
            _container.Instance<IDatabase>(new Database(DbPath, DbConnection)); //Register the Sqlite Apis -- This is found in the infrastructure directory

            // TODO: Register any Platform-Specific abstractions here
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return new[]
            {
                GetType().Assembly,
                typeof(App).Assembly
            };
        }

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
    }
}
