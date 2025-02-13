using System.IO;
using System.Text.Json;
using Xsort.WPF.Models;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.Services;

public class SettingsService : ISettingsService
{
    private const string SettingsFileName = "settings.json";
    public AppSettings CurrentSettings { get; }

    public SettingsService()
    {
        CurrentSettings = LoadSettings();
    }
    
    public AppSettings LoadSettings()
    {
        if (!File.Exists(SettingsFileName))
        {
            return new AppSettings();
        }
        
        var json = File.ReadAllText(SettingsFileName);
        return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
    }

    public void SaveSettings()
    {
        var json = JsonSerializer.Serialize(CurrentSettings, new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(SettingsFileName, json);
    }

    public void UpdateSettings(Action<AppSettings> updateSettingsAction)
    {
        updateSettingsAction(CurrentSettings);
        SaveSettings();
    }
}