using System.Linq;
using System.Text.RegularExpressions;

namespace Xsort
{
    internal class Program
    {
        //language=regex
        private const string Pattern = @" +([0-9]{2,4})(\.|-)([0-9]{2})(\.|-)([0-9]{2,4}).+\.*";
        private static readonly string[] Exts = { ".png", ".jpg", ".mp4" };
        private static readonly string BaseDirectory = Environment.CurrentDirectory;

        static void Main(string[] args)
        {
            if (Directory.Exists(BaseDirectory))
            {
                foreach (var file in new DirectoryInfo(BaseDirectory).GetFiles().Where(f => Exts.Contains(f.Extension)))
                {
                    var folderName = Regex.Replace(file.Name, Pattern, String.Empty).Trim();
                    Directory.CreateDirectory($@"{BaseDirectory}\{folderName}");
                    file.MoveTo($@"{BaseDirectory}\{folderName}\{file.Name}");
                }
            }
        }
    }
}