namespace Xsort.WPF.Services.Interfaces;

public interface ISettingsService
{
    AppSettings LoadSettings();
    void SaveSettings(AppSettings settings);
}