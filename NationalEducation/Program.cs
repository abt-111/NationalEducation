using Newtonsoft.Json;

namespace NationalEducation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Replace("\\bin\\Debug\\net8.0", "");

            string userInput;

            // Read the JSON string from the file
            var jsonString = File.ReadAllText($"{path}\\save.JSON");

            // Deserialize the JSON string to an instance of SchoolApp

            AppData appData = JsonConvert.DeserializeObject<AppData>(jsonString);

            CampusApp campusApp = new CampusApp(appData);

            MenuApp.LaunchMenuApp(campusApp);

            File.WriteAllText($"{path}\\save.JSON", JsonConvert.SerializeObject(appData, Formatting.Indented));
        }
    }
}
