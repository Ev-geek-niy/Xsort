using Xsort.WPF.Models;

namespace Xsort.WPF.Services.Interfaces;

public interface ISettingsService
{
    AppSettings LoadSettings();
    void SaveSettings();
    void UpdateSettings(Action<AppSettings> updateSettingsAction);
    AppSettings CurrentSettings { get; }
}