using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Win32;
using XsortApp.Annotations;

namespace XsortApp.MVVM.Model;

public class Settings : INotifyPropertyChanged
{
    //language=regex
    private string _pattern = @" +([0-9]{2,4})(\.|-)([0-9]{2})(\.|-)([0-9]{2,4}).+\.*";
    private string? _path;
    private string[]? _exts;
    private bool _isAutoStartup;
    private bool _isMinimized;

    public Settings()
    {
        Path = Registry.CurrentUser.OpenSubKey("Xsort")?.GetValue("Path")?.ToString();
        Exts = new[] { ".png" };
        IsAutoStartup = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run")?.GetValue("Xsort") != null;
        IsMinimized = Registry.CurrentUser.OpenSubKey("Xsort")?.GetValue("minimize").Equals("True") ?? false;
    }

    public string Pattern
    {
        get { return _pattern; }
        set
        {
            _pattern = value;
            OnPropertyChanged(nameof(Pattern));
        }
    }

    public string? Path
    {
        get { return _path; }
        set
        {
            _path = value;
            OnPropertyChanged(nameof(Path));
        }
    }

    public string[]? Exts
    {
        get { return _exts; }
        set
        {
            _exts = value;
            OnPropertyChanged(nameof(Exts));
        }
    }

    public bool IsAutoStartup
    {
        get { return _isAutoStartup; }
        set
        {
            _isAutoStartup = value;
            OnPropertyChanged(nameof(IsAutoStartup));
        }
    }

    public bool IsMinimized
    {
        get { return _isMinimized; }
        set
        {
            _isMinimized = value;
            OnPropertyChanged(nameof(IsMinimized));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}