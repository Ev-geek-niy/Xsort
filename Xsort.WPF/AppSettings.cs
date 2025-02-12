namespace Xsort.WPF;

public class AppSettings
{
    public bool IsMinimized { get; set; } = false;
    public bool IsAutoStartup { get; set; } = false;
    public string FolderPath { get; set; } = string.Empty;
    public List<string> Extensions { get; set; } = [".png"];
    public string Pattern = @" +([0-9]{2,4})(\.|-)([0-9]{2})(\.|-)([0-9]{2,4}).+\.*";
}