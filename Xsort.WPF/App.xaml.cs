using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xsort.WPF.Models;
using Xsort.WPF.Services;
using Xsort.WPF.Services.Interfaces;
using Xsort.WPF.ViewModels;

namespace Xsort.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;
    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<AppSettings>();
                RegisterUI(services);
                RegisterServices(services);
                RegisterViewModels(services);
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
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