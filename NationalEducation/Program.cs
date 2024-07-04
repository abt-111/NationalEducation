using NationalEducation.Operators;

namespace NationalEducation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            FileOperator.GeneratePath();

            FileOperator.LogTest();

            FileOperator.LoadData(out DataApp appData);

            CampusApp campusApp = new CampusApp(appData);

            campusApp.LaunchApp();

            FileOperator.SaveData(appData);
        }
    }
}
