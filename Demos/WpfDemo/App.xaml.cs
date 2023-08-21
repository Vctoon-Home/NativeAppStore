using Microsoft.Extensions.DependencyInjection;
using NativeAppStore;
using NativeAppStore.Extensions;
using System.Windows;

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
            StoreSaveExecutor.SaveAllStores();
            base.OnExit(e);
        }
    }
}