using System;
using System.Windows;
using System.Windows.Input;
using XsortApp.MVVM.Model;
using XsortApp.Services;

namespace XsortApp.MVVM.ViewModel;

public sealed class ApplicationViewModel
{
    public Settings AppSettings { get; }


    public ApplicationViewModel()
    {
        AppSettings = new Settings();
    }

    public ICommand ChangeDirectoryPathCommand
    {
        get
        {
            return new DelegateCommand((obj) =>
            {
                string path = ExplorerService.GetFullPathByDialog();
                RegistryService.SetKeyValueRegistry("path", path);
                AppSettings.Path = RegistryService.GetKeyValueRegistry("path");
            });
        }
    }

    public ICommand SortFileInFolderCommand
    {
        get
        {
            return new DelegateCommand((obj) =>
            {
                try
                {
                    ExplorerService.SortFiles(AppSettings);
                    MessageBox.Show("Все файлы успешно сортированы");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Кажется, что-то пошло не так");
                    throw;
                }

            });
        }
    }

    public ICommand AutoStartupCommand
    {
        get
        {
            return new DelegateCommand((obj) =>
            {
                RegistryService.SetStartupRegistry(AppSettings.IsAutoStartup);
            });
        }
    }
}