namespace NationalEducation
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            // string path = Directory.GetCurrentDirectory();
            // path = path.Replace("\\bin\\Debug\\net8.0", "");

            FileOperator.LoadData(out AppData appData);

            FileOperator.LogTest();

            CampusApp campusApp = new CampusApp(appData);

            campusApp.DisplayStudent();

            // MenuApp.LaunchMenuApp(campusApp);

            // FileOperator.SaveData(appData);
        }
    }
}
