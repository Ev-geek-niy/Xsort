using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xsort.Services.Services;
using Xsort.Services.Services.Interfaces;

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
                services.AddSingleton<MainWindow>();
                services.AddTransient<IRegistryService, RegistryService>();
            })
            .Build();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.WindowState = WindowState.Minimized;
        mainWindow.ShowInTaskbar = false;
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        try
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
        catch (Exception ex)
        {
            throw; // TODO handle exception
        }
    }
}