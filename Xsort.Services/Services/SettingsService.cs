using System.Text.Json;
using Microsoft.Extensions.Logging;
using Xsort.Core.Interfaces;
using Xsort.Core.Models;
using Xsort.Infrastructure.FileStorage;

namespace Xsort.Services.Services;

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

        return JsonFileStorage.Load<AppSettings>(SettingsPath) ?? new AppSettings();
    }

    public void SaveSettings()
    {
        JsonFileStorage.Save(SettingsPath, CurrentSettings);
    }

    public void UpdateSettings(Action<AppSettings> updateSettingsAction)
    {
        updateSettingsAction(CurrentSettings);
        SaveSettings();
    }
}