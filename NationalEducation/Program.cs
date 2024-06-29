namespace NationalEducation
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            //string path = Directory.GetCurrentDirectory();
            //path = path.Replace("\\bin\\Debug\\net8.0", "");
            //Console.WriteLine(path);

            FileOperator.LoadData(out AppData appData);

            FileOperator.LogTest();

            CampusApp campusApp = new CampusApp(appData);

            MenuApp.LaunchMenuApp(campusApp);

            FileOperator.SaveData(appData);
        }
    }
}
