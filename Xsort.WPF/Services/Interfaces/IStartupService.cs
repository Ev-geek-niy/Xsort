namespace Xsort.WPF.Services.Interfaces;

public interface IStartupService
{
    void EnableStartup();
    void DisableStartup();
    bool IsStartupEnabled();
}