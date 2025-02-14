using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Xsort.WPF.Models;
using Xsort.WPF.Services;
using Xsort.WPF.Services.Interfaces;
using Xsort.WPF.ViewModels;
using Serilog;

namespace Xsort.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    private static Mutex? _mutex;
    public App()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSerilog();
                services.AddSingleton<AppSettings>();
                RegisterUI(services);
                RegisterServices(services);
                RegisterViewModels(services);
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        const string mutexName = "Global\\Xsort";
        
        _mutex = new Mutex(true, mutexName, out bool createdNew);

        if (!createdNew)
            Environment.Exit(0);
        
        DispatcherUnhandledException += OnDispatcherUnhandledException;
        base.OnStartup(e);
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
    }

    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Error(e.Exception, "Unhandled exception");
        MessageBox.Show(e.Exception.Message);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await _host.StopAsync();
        _host.Dispose();
        base.OnExit(e);
    }

    private static void RegisterUI(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
    }

    private void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IStartupService, StartupService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IExplorerService, ExplorerService>();
        services.AddSingleton<IFolderWatcherService, FolderWatcherService>();
    }

    private void RegisterViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
    }
    
    
}