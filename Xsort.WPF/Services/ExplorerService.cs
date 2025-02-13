using System.IO;
using System.Text.RegularExpressions;
using Xsort.WPF.Extensions;
using Xsort.WPF.Models;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.Services;

public class ExplorerService : IExplorerService
{
    private readonly AppSettings _settings;

    public ExplorerService(ISettingsService settingsService)
    {
        _settings = settingsService.CurrentSettings;
    }

    public async Task SortFiles()
    {
        if (Directory.Exists(_settings.FolderPath))
        {
            var files = new DirectoryInfo(_settings.FolderPath)
                .GetFiles()
                .Where(f => _settings.Extensions.Contains(f.Extension))
                .ToList();
            foreach (var file in files)
            {
                var folderName = Regex.Replace(file.Name, AppSettings.Pattern, string.Empty).Trim();
                Directory.CreateDirectory($@"{_settings.FolderPath}\{folderName}");

                try
                {
                    file.MoveTo($@"{_settings.FolderPath}\{folderName}\{file.Name}");
                }
                catch (IOException)
                {
                    await Task.Delay(1000);
                    await file.MoveWithRetry($@"{_settings.FolderPath}\{folderName}\{file.Name}");
                }
            }
        }
        else
        {
            throw new DirectoryNotFoundException();
        }
    }
}