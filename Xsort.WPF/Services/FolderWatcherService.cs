using System.IO;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.Services;

public class FolderWatcherService : IFolderWatcherService
{
    private readonly AppSettings _settings;
    private readonly IExplorerService _explorerService;
    private FileSystemWatcher? _watcher;

    public FolderWatcherService(ISettingsService settingsService, IExplorerService explorerService)
    {
        _explorerService = explorerService;
        _settings = settingsService.LoadSettings();
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

        _watcher.Changed += (sender, args) => _explorerService.SortFiles();
        
        _watcher.EnableRaisingEvents = true;
    }

    public void ChangePath(string path)
    {
        _settings.FolderPath = path;
        StartWatch();
    }
}