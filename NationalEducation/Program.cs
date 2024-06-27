using Newtonsoft.Json;

namespace NationalEducation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // A revoir
            /*string path = Directory.GetCurrentDirectory();
            path = path.Replace("\\bin\\Debug\\net8.0", "");*/

            LoadData(out AppData appData);

            CampusApp campusApp = new CampusApp(appData);

            MenuApp.LaunchMenuApp(campusApp);

            SaveData(appData);
        }

        public static void SaveData(AppData appData)
        {
            // Serialize the instance to a JSON string
            string jsonString = JsonConvert.SerializeObject(appData, Formatting.Indented);

            // Write the JSON string to a file
            File.WriteAllText(ConstantValue.JSON_FILE_PATH, jsonString);
        }

        public static void LoadData(out AppData appData)
        {
            // Read the JSON string from the file
            string jsonString = File.ReadAllText(ConstantValue.JSON_FILE_PATH);

            // Deserialize the JSON string to an instance of SchoolApp
            appData = JsonConvert.DeserializeObject<AppData>(jsonString);
        }
    }
}
