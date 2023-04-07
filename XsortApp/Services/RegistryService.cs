using System;
using Microsoft.Win32;

namespace XsortApp.Services;

public static class RegistryService
{
    public static void SetKeyValueRegistry(string key, string valueName)
    {
        var currentUser = Registry.CurrentUser;
        var XsortRegistry = currentUser.CreateSubKey("Xsort");
        XsortRegistry.SetValue(key, valueName);
    }

    public static string? GetKeyValueRegistry(string valueName)
    {
        return Registry.CurrentUser.OpenSubKey("Xsort")?.GetValue(valueName)?.ToString();
    }

    public static void SetExtRegistry()
    {
        throw new NotImplementedException();
    }
}