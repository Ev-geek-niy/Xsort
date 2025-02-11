using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Win32;
using Xsort.WPF.Commands;
using Xsort.WPF.Services.Interfaces;

namespace Xsort.WPF.ViewModels;

public sealed class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly ISettingsService _settingsService;
    private readonly AppSettings _settings;
    public string FolderPath
    {
        get => _settings.FolderPath;
        set
        {
            if (_settings.FolderPath == value)
                return;
            
            _settings.FolderPath = value;
            _settingsService.SaveSettings(_settings);
            OnPropertyChanged();
        }
    }

    public bool IsAutoStartUp
    {
        get => _settings.IsAutoStartUp;
        set
        {
            if (_settings.IsAutoStartUp == value)
                return;
            
            _settings.IsAutoStartUp = value;
            _settingsService.SaveSettings(_settings);
            OnPropertyChanged();
        }
    }
    
    public bool IsMinimized
    {
        get => _settings.IsMinimized;
        set
        {
            if (_settings.IsMinimized == value)
                return;
            
            _settings.IsMinimized = value;
            _settingsService.SaveSettings(_settings);
            OnPropertyChanged();
        }
    }

    public ICommand SetFolderPathCommand { get; set; }

    public MainWindowViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        _settings = settingsService.LoadSettings();
        SetFolderPathCommand = new RelayCommand(GetFolderPath);
    }

    private void GetFolderPath(object? parameters)
    {
        var openFolderDialog = new OpenFolderDialog()
        {
            Title = "Выберите путь до папки"
        };

        FolderPath = openFolderDialog.ShowDialog() == true
            ? openFolderDialog.FolderName
            : string.Empty;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}