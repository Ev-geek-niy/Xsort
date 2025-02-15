using Xsort.Core.Interfaces;
using Xsort.Core.Models;

namespace Xsort.Services.Services;

public class FolderWatcherService : IFolderWatcherService
{
    private readonly AppSettings _settings;
    private readonly IExplorerService _explorerService;
    private FileSystemWatcher? _watcher;

    public FolderWatcherService(ISettingsService settingsService, IExplorerService explorerService)
    {
        _explorerService = explorerService;
        _settings = settingsService.CurrentSettings;
    }
    
    public void StartWatch()
    {
        if (_watcher != null)
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
        }
        
        _watcher = new FileSystemWatcher(_settings.FolderPath)
            {NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.DirectoryName};
        _watcher.Changed += (sender, args) =>
        {
            _watcher.EnableRaisingEvents = false;
            try
            {
                _explorerService.SortFiles();
            }
            finally
            {
                _watcher.EnableRaisingEvents = true;
            }
        };
        _watcher.EnableRaisingEvents = true;
    }

    public void ChangePath(string path)
    {
        _settings.FolderPath = path;
        StartWatch();
    }
}