using NationalEducation.Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System.IO;

namespace NationalEducation
{
    internal static class FileOperator
    {
        // Variables
        public static string projectPath = "";
        public static string jsonFilePath ="";
        public static string logFilePath = "";
        // Constantes
        public const string JSON_FILE_NAME = "SaveAndLog\\campusApp.JSON";
        public const string LOG_FILE_NAME = "SaveAndLog\\campusApp.log";

        public static void GeneratePath()
        {
            projectPath = Directory.GetCurrentDirectory();
            projectPath = projectPath.Replace("\\bin\\Debug\\net8.0", "");
            jsonFilePath = $"{projectPath}\\{JSON_FILE_NAME}";
            logFilePath = $"{projectPath}\\{LOG_FILE_NAME}";
        }

        public static void SaveData(AppData appData)
        {
            // Convertion de l'instance de AppData en chaîne de caractères formatée pour
            // l'enregistrement dans un fichier JSON
            string jsonString = JsonConvert.SerializeObject(appData, Formatting.Indented);

            // Ecriture de la chaîne de caractères dans un fichier JSON
            File.WriteAllText(jsonFilePath, jsonString);
        }

        public static void LoadData(out AppData appData)
        {
            if(File.Exists(jsonFilePath))
            {
                // Lecture du contenue du fichier JSON et affection dans une chaîne de caractères
                string jsonString = File.ReadAllText(jsonFilePath);

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
                .WriteTo.File(logFilePath, LogEventLevel.Information)
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}
