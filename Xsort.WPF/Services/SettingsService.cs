using System.IO;
using System.Net;
using System.Text.Json;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.Services;

public class SettingsService : ISettingsService
{
    private const string SettingsFileName = "settings.json";
    
    public AppSettings LoadSettings()
    {
        if (!File.Exists(SettingsFileName))
        {
            return new AppSettings();
        }
        
        var json = File.ReadAllText(SettingsFileName);
        return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
    }

    public void SaveSettings(AppSettings settings)
    {
        var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(SettingsFileName, json);
    }
}