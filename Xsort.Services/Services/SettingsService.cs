using System.Text.Json;
using Microsoft.Extensions.Logging;
using Xsort.Core.Helpers;
using Xsort.Core.Interfaces;
using Xsort.Core.Models;
using Xsort.Infrastructure.FileStorage;

namespace Xsort.Services.Services;

public class SettingsService : ISettingsService
{
    private readonly ILogger<SettingsService> _logger;
    private const string FileName = "settings.json";
    private static readonly string DirectoryPath = DebugHelper.IsDebug() 
        ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
        : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Xsort");
    private static readonly string FullPath = Path.Combine(DirectoryPath, FileName);
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
            FullPath);

        Directory.CreateDirectory(DirectoryPath);
        return JsonFileStorage.Load<AppSettings>(FullPath) ?? new AppSettings();
    }

    public void SaveSettings()
    {
        JsonFileStorage.Save(FullPath, CurrentSettings);
    }

    public void UpdateSettings(Action<AppSettings> updateSettingsAction)
    {
        updateSettingsAction(CurrentSettings);
        SaveSettings();
    }
}