namespace Xsort.Core.Models;

public class AppSettings
{
    public bool IsMinimized { get; set; }
    public bool IsAutoStartup { get; set; }
    public string FolderPath { get; set; } = string.Empty;
    public List<string> Extensions { get; init; } = [".png"];
    public const string Pattern = @" +([0-9]{2,4})(\.|-)([0-9]{2})(\.|-)([0-9]{2,4}).+\.*";
}