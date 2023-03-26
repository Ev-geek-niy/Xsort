using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Linq;
using System.Text.RegularExpressions;

namespace Xsort
{
    internal class Program
    {
        //language=regex
        private const string Pattern = @" +([0-9]{2,4})(\.|-)([0-9]{2})(\.|-)([0-9]{2,4}).+\.*";
        private static readonly string[] Exts = { ".png", ".jpg", ".mp4" };

        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var targetFolder = config["TargetFolder"];

            Console.WriteLine("Запуск приложения...");
            if (Directory.Exists(targetFolder))
            {
                foreach (var file in new DirectoryInfo(targetFolder).GetFiles().Where(f => Exts.Contains(f.Extension)))
                {
                    var folderName = Regex.Replace(file.Name, Pattern, String.Empty).Trim();
                    Directory.CreateDirectory($@"{targetFolder}\{folderName}");
                    file.MoveTo($@"{targetFolder}\{folderName}\{file.Name}");
                }
            }
            Console.WriteLine("Все готово! Нажмите любую клавишу, чтобы закончить.");
            Console.ReadLine();
        }
    }
}