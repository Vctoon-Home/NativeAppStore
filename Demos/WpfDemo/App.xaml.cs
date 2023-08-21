using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using NativeAppStore;
using NativeAppStore.Extensions;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceCollection ServiceCollection;

        public static IServiceProvider Services;

        public App()
        {
            ServiceCollection = new ServiceCollection();

            ServiceCollection.AddStores(GetType().Assembly, opt => { opt.EnabledCreatorStoreLoad = true; });

            Services = ServiceCollection.BuildServiceProvider();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            StoreSaveExecutor.SaveStore();
            base.OnExit(e);
        }
    }
}