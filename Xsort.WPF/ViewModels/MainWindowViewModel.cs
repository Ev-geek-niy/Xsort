using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Win32;
using Xsort.WPF.Commands;
using Xsort.WPF.Models;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.ViewModels;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly ISettingsService _settingsService;
    private readonly IStartupService _startupService;
    private readonly IFolderWatcherService _folderWatcherService;

    private string _folderPath;
    private bool _isAutostartup;
    private bool _isMinimized;
    
    public MainWindowViewModel(ISettingsService settingsService, IStartupService startupService, IFolderWatcherService folderWatcherService)
    {
        _settingsService = settingsService;
        _startupService = startupService;
        _folderWatcherService = folderWatcherService;

        _folderPath = _settingsService.CurrentSettings.FolderPath;
        _isAutostartup = _settingsService.CurrentSettings.IsAutoStartup;
        _isMinimized = _settingsService.CurrentSettings.IsMinimized;
        
        SetFolderPathCommand = new RelayCommand(GetFolderPath);
        SetAutoStartupCommand = new RelayCommand(SetAutoStartup);
        
        if (!string.IsNullOrEmpty(FolderPath))
            _folderWatcherService.StartWatch();
    }
    
    public string FolderPath
    {
        get => _folderPath;
        set
        {
            if (_folderPath == value)
                return;
            
            _folderPath = value;
            _settingsService.UpdateSettings(s => s.FolderPath = _folderPath);
            OnPropertyChanged();
        }
    }

    public bool IsAutoStartup
    {
        get => _isAutostartup;
        set
        {
            if (_isAutostartup == value)
                return;
            
            _isAutostartup = value;
            _settingsService.UpdateSettings(s => s.IsAutoStartup = _isAutostartup);
            OnPropertyChanged();
        }
    }
    
    public bool IsMinimized
    {
        get => _isMinimized;
        set
        {
            if (_isMinimized == value)
                return;
            
            _isMinimized = value;
            _settingsService.UpdateSettings(s => s.IsMinimized = _isMinimized);
            OnPropertyChanged();
        }
    }

    public ICommand SetFolderPathCommand { get; set; }
    public ICommand SetAutoStartupCommand { get; set; }

    private void GetFolderPath(object? parameters)
    {
        var openFolderDialog = new OpenFolderDialog()
        {
            Title = "Выберите путь до папки"
        };

        FolderPath = openFolderDialog.ShowDialog() == true
            ? openFolderDialog.FolderName
            : string.Empty;
        
        _folderWatcherService.ChangePath(FolderPath);
    }

    private void SetAutoStartup(object? parameters)
    {
        if (_startupService.IsStartupEnabled())
        {
            _startupService.DisableStartup();
            IsAutoStartup = false;            
        }
        else
        {
            _startupService.EnableStartup();
            IsAutoStartup = true;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}