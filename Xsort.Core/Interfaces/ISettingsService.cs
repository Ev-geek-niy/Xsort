using Xsort.Core.Models;

namespace Xsort.Core.Interfaces;

public interface ISettingsService
{
    AppSettings LoadSettings();
    void SaveSettings();
    void UpdateSettings(Action<AppSettings> updateSettingsAction);
    AppSettings CurrentSettings { get; }
}