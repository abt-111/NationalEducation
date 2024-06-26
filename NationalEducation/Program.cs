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

            // Menu
            // Au lancement de l'application, un menu permettra à l'utilisateur de choisir entre ces entrées :
            // 
            // Etudiants
            // Cours
            do
            {
                Console.WriteLine("\nApplication révolutionnaire\n");
                Console.WriteLine("0 : Etudiants");
                Console.WriteLine("1 : Cours");
                Console.WriteLine();

                Console.Write("Entrées : ");
                userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    do
                    {
                        // Le menu Elèves permettra quant à lui de :
                        // 
                        // Lister les étudiants
                        // Créer un nouvel étudiants
                        // Consulter un étudiants existant
                        // Ajouter une note et une appréciation pour un cours sur un étudiants existant
                        // Revenir au menu principal
                        Console.WriteLine("\nApplication révolutionnaire\n");
                        Console.WriteLine("0 : Lister les étudiants");
                        Console.WriteLine("1 : Créer un nouvel étudiants");
                        Console.WriteLine("2 : Consulter un étudiants existant");
                        Console.WriteLine("3 : Ajouter une note et une appréciation pour un cours sur un étudiants existant");
                        Console.WriteLine("4 : Revenir au menu principal");
                        Console.WriteLine();

                        Console.Write("Entrées : ");
                        userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "0":
                                campusApp.ListAllStudents();
                                break;
                            case "1":
                                campusApp.CreateNewStudent();
                                break;
                            case "2":
                                campusApp.DisplayStudent();
                                break;
                            case "3":
                                campusApp.AddGradeToStudent();
                                break;
                            default:
                                break;
                        }
                    }
                    while (userInput != "exit" && userInput != "4");
                }
                else if (userInput == "1")
                {
                    do
                    {
                        // Le menu Cours permettra de son côté de :
                        // 
                        // Lister les cours existants
                        // Ajouter un nouveau cours au programme
                        // Supprimer un cours par son identifiant
                        // Revenir au menu principal
                        Console.WriteLine("\nApplication révolutionnaire\n");
                        Console.WriteLine("0 : Lister les cours existants");
                        Console.WriteLine("1 : Ajouter un nouveau cours au programme");
                        Console.WriteLine("2 : Supprimer un cours");
                        Console.WriteLine("3 : Revenir au menu principal");
                        Console.WriteLine();

                        Console.Write("Entrées : ");
                        userInput = Console.ReadLine();

                        switch (userInput)
                        {
                            case "0":
                                campusApp.ListAllCourses();
                                break;
                            case "1":
                                campusApp.CreateNewCourse();
                                break;
                            case "2":
                                campusApp.DeleteCourse();
                                break;
                            default:
                                break;
                        }
                    }
                    while (userInput != "exit" && userInput != "3");
                }
            }
            while (userInput != "exit");

            File.WriteAllText($"{path}\\test.JSON", JsonConvert.SerializeObject(appData, Formatting.Indented));
        }
    }
}
