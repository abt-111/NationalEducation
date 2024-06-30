namespace NationalEducation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            FileOperator.GeneratePath();

            FileOperator.LogTest();

            FileOperator.LoadData(out AppData appData);

            CampusApp campusApp = new CampusApp(appData);

            MenuApp.LaunchMenuApp(campusApp);

            FileOperator.SaveData(appData);
        }
    }
}
