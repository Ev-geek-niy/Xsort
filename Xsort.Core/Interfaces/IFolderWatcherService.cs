namespace Xsort.Core.Interfaces;

public interface IFolderWatcherService
{
    void StartWatch();
    void ChangePath(string path);
}