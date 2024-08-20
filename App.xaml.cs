using System.Windows;
using JsonFilterCase.Properties;
using JsonFilterCase.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace JsonFilterCase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(Settings.Default.LogPath, rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton(s => new MainWindow(s.GetRequiredService<MainViewModel>()));
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();

            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs exitEventArgs)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(exitEventArgs);
        }
    }
}
