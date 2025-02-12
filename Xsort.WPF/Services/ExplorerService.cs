using System.IO;
using System.Text.RegularExpressions;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.Services;

public class ExplorerService : IExplorerService
{
    private readonly AppSettings _settings;

    public ExplorerService(ISettingsService settingsService)
    {
        _settings = settingsService.LoadSettings();
    }

    public Task SortFiles()
    {
        if (Directory.Exists(_settings.FolderPath))
        {
            foreach (var file in new DirectoryInfo(_settings.FolderPath).GetFiles().Where(f => _settings.Extensions.Contains(f.Extension)))
            {
                var folderName = Regex.Replace(file.Name, _settings.Pattern, string.Empty).Trim();
                Directory.CreateDirectory($@"{_settings.FolderPath}\{folderName}");
                file.MoveTo($@"{_settings.FolderPath}\{folderName}\{file.Name}");
            }
        }
        else
        {
            throw new DirectoryNotFoundException();
        }
        
        return Task.CompletedTask;
    }
}