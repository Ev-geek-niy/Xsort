using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Input;
using XsortApp.MVVM.Model;

namespace XsortApp.Services;

public static class ExplorerService
{
    public static string GetFullPathByDialog()
    {
        var dialog = new FolderBrowserDialog();
        dialog.ShowDialog();

        return dialog.SelectedPath;
    }

    public static void SortFiles(Settings settings)
    {
        if (Directory.Exists(settings.Path))
        {
            foreach (var file in new DirectoryInfo(settings.Path).GetFiles().Where(f => settings.Exts != null && settings.Exts.Contains(f.Extension)))
            {
                var folderName = Regex.Replace(file.Name, settings.Pattern, string.Empty).Trim();
                Directory.CreateDirectory($@"{settings.Path}\{folderName}");
                file.MoveTo($@"{settings.Path}\{folderName}\{file.Name}");
            }
        }
        else
        {
            throw new DirectoryNotFoundException();
        }
    }

}