using System.Runtime.Versioning;
using Microsoft.Win32;
using Xsort.Core.Interfaces;

namespace Xsort.Services.Services;

public class WindowsStartupService : IStartupService
{
    private const string ApplicationName = "Xsort";
    private readonly string _applicationPath = Environment.ProcessPath ?? throw new Exception("No process path");
    private const string RegistryPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

    [SupportedOSPlatform("Windows")]
    public void EnableStartup()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
        key?.SetValue(ApplicationName, $"\"{_applicationPath}\"");
    }

    [SupportedOSPlatform("Windows")]
    public void DisableStartup()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
        key?.DeleteValue(ApplicationName, false);
    }

    [SupportedOSPlatform("Windows")]
    public bool IsStartupEnabled()
    {
        using var key = Registry.CurrentUser.OpenSubKey(RegistryPath, true);
        return key?.GetValue(ApplicationName) != null;
    }
}