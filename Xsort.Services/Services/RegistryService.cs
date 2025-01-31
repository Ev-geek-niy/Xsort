using System.Diagnostics;
using Microsoft.Win32;
using Xsort.Services.Services.Interfaces;

namespace Xsort.Services.Services;

public class RegistryService : IRegistryService
{
    public Task SetKeyValueRegistry(string key, object valueName)
    {
        var currentUser = Registry.CurrentUser;
        var XsortRegistry = currentUser.CreateSubKey("Xsort");
        XsortRegistry.SetValue(key, valueName);
        return Task.CompletedTask;
    }

    public Task<string> GetKeyValueRegistry(string valueName)
    {
        return Task.FromResult(Registry.CurrentUser.OpenSubKey("Xsort")?.GetValue(valueName)?.ToString());
    }

    public Task SetExtRegistry()
    {
        throw new NotImplementedException();
    }

    public Task SetStartupRegistry(bool isAutoStartup)
    {
        string path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
        var key = Registry.CurrentUser.OpenSubKey(path, true);
        if (isAutoStartup)
            key.SetValue("Xsort", Process.GetCurrentProcess().MainModule.FileName + "--autostart");
        else
            key.DeleteValue("Xsort", false);
        
        return Task.CompletedTask;
    }

    public void PrintHello()
    {
        Console.WriteLine("Hello");
    }
}