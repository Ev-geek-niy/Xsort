using Microsoft.Win32;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.Services;

public class StartupService : IStartupService
{
    private const string ApplicationName = "Xsort";
    private readonly string ApplicationPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
    private readonly string RegistryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
    
    public void EnableStartup()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
        key?.SetValue(ApplicationName, $"\"{ApplicationPath}\"");
    }

    public void DisableStartup()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
        key?.DeleteValue(ApplicationName, false);
    }

    public bool IsStartupEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
        return key?.GetValue(ApplicationName) != null;
    }
}