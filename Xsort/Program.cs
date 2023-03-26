using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Xsort
{
    internal class Program
    {
        public static void AddOrUpdateAppSetting<T>(string key, T value)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appSettings.json");
                string json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                jsonObj[key] = value; // if no sectionpath just set the value

                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        //language=regex
        private const string Pattern = @" +([0-9]{2,4})(\.|-)([0-9]{2})(\.|-)([0-9]{2,4}).+\.*";
        private static readonly string[] Exts = { ".png", ".jpg", ".mp4" };

        static void Main(string[] args)
        {
            Console.WriteLine("Запуск приложения...");

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            string targetFolder = config["targetFolder"];

            while (String.IsNullOrEmpty(config["targetFolder"]))
            {
                Console.WriteLine("Напишите полный путь до необходимой папки:");
                string folder = Console.ReadLine();
                AddOrUpdateAppSetting("targetFolder", folder);
                config["targetFolder"] = folder;
            }

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

            Console.WriteLine("Все готово! Нажмите любую клавишу, чтобы закончить.");
            Console.ReadLine();
        }
    }
}