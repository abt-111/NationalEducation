using NationalEducation.Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System.IO;

namespace NationalEducation
{
    internal static class FileOperator
    {
        public static void SaveData(AppData appData)
        {
            // Convertion de l'instance de AppData en chaîne de caractères formatée pour
            // l'enregistrement dans un fichier JSON
            string jsonString = JsonConvert.SerializeObject(appData, Formatting.Indented);

            // Ecriture de la chaîne de caractères dans un fichier JSON
            File.WriteAllText(ConstantValue.JSON_FILE_PATH, jsonString);
        }

        public static void LoadData(out AppData appData)
        {
            if(File.Exists(ConstantValue.JSON_FILE_PATH))
            {
                // Lecture du contenue du fichier JSON et affection dans une chaîne de caractères
                string jsonString = File.ReadAllText(ConstantValue.JSON_FILE_PATH);

                // Convertion de la chaîne de caractères en une instance de AppData
                appData = JsonConvert.DeserializeObject<AppData>(jsonString);
            }
            else
            {
                appData = new AppData(new List<Student>(), new List<Course>(), new List<Grade>());
            }
        }

        public static void LogTest()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(ConstantValue.LOG_FILE_PATH, LogEventLevel.Information)
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
