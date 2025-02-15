using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Xsort.Core.Helpers;
using Xsort.Core.Interfaces;
using Xsort.WPF.ViewModels;
using Xsort.Core.Models;
using Xsort.Infrastructure.Logging;
using Xsort.Services.Services;

namespace Xsort.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    private static Mutex? _mutex;
    private readonly ILogger<App> _logger;
    public App()
    {
        var configuration = DebugHelper.IsDebug()
            ? new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
            : new ConfigurationBuilder().Build();

        var loggerFactory = SerilogLogger.CreateLoggerFactory(configuration);
        _logger = loggerFactory.CreateLogger<App>();
        
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton(loggerFactory);
                services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
                RegisterUI(services);
                RegisterServices(services);
                RegisterViewModels(services);
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mutexName = DebugHelper.IsDebug() ? "Xsort" : "Debug-Xsort";
        
        _mutex = new Mutex(true, mutexName, out var createdNew);

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
        services.AddSingleton<IStartupService, WindowsStartupService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IExplorerService, ExplorerService>();
        services.AddSingleton<IFolderWatcherService, FolderWatcherService>();
    }

    private void RegisterViewModels(IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
    }
    
    
}