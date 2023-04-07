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

    public Settings(string? path = null, string[]? exts = null)
    {
        Path = path ?? Registry.CurrentUser.OpenSubKey("Xsort")?.GetValue("Path")?.ToString();
        Exts = exts ?? new[] { ".png" };
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

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}