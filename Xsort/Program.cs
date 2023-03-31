using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Xsort
{
    internal static class Program
    {
        //language=regex
        private const string Pattern = @" +([0-9]{2,4})(\.|-)([0-9]{2})(\.|-)([0-9]{2,4}).+\.*";
        private static readonly string[] Exts = { ".png", ".jpg", ".mp4" };

        private static string? CreateOrGetFolderPath()
        {
            var currentUser = Registry.CurrentUser;
            var XsortRegistry = currentUser.CreateSubKey("Xsort");

            while (XsortRegistry.GetValue("path") == null)
            {
                Console.WriteLine("Напишите полный путь до необходимой папки:");
                string? folderPath = Console.ReadLine();
                XsortRegistry.SetValue("path", folderPath);
            }

            return XsortRegistry.GetValue("path")?.ToString();
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Запуск приложения...");
            string? targetFolder = CreateOrGetFolderPath();

            Console.WriteLine($"Сортируем все файлы по пути: {targetFolder}");

            if (Directory.Exists(targetFolder))
            {
                foreach (var file in new DirectoryInfo(targetFolder).GetFiles().Where(f => Exts.Contains(f.Extension)))
                {
                    var folderName = Regex.Replace(file.Name, Pattern, String.Empty).Trim();
                    Directory.CreateDirectory($@"{targetFolder}\{folderName}");
                    file.MoveTo($@"{targetFolder}\{folderName}\{file.Name}");
                }
            }
            else
            {
                Console.WriteLine("Такой папки не существует.");
            }

            Console.WriteLine("Все готово! Нажмите любую клавишу, чтобы закончить.");
            Console.ReadLine();
        }
    }
}