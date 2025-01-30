using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xsort.WPF;

namespace Xsort.Core;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(services);
            })
            .Build();

        var app = new App();
        var mainWindow = host.Services.GetRequiredService<MainWindow>();

        app.Run(mainWindow);
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
    }
}