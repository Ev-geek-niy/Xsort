using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Xsort.WPF.Models;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.Services;

public class SettingsService : ISettingsService
{
    private readonly ILogger<SettingsService> _logger;
    private static readonly string SettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
    public AppSettings CurrentSettings { get; }

    public SettingsService(ILogger<SettingsService> logger)
    {
        _logger = logger;
        CurrentSettings = LoadSettings();
        _logger.LogInformation("Текущие настройки: {CurrentSettings}",
            JsonSerializer.Serialize(CurrentSettings));
    }
    
    public AppSettings LoadSettings()
    {
        _logger.LogInformation("Поиск файла с настройками: {SettingsPath}", 
            SettingsPath);
        
        if (!File.Exists(SettingsPath))
        {
            return new AppSettings();
        }
        
        var json = File.ReadAllText(SettingsPath);
        return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
    }

    public void SaveSettings()
    {
        var json = JsonSerializer.Serialize(CurrentSettings, new JsonSerializerOptions {WriteIndented = true});
        File.WriteAllText(SettingsPath, json);
    }

    public void UpdateSettings(Action<AppSettings> updateSettingsAction)
    {
        updateSettingsAction(CurrentSettings);
        SaveSettings();
    }
}