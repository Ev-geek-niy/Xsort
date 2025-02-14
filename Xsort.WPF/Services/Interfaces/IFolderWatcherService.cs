namespace Xsort.WPF.Services.Interfaces;

public interface IFolderWatcherService
{
    void StartWatch();
    void ChangePath(string path);
}