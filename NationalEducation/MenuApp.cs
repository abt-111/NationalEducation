namespace NationalEducation
{
    internal static class MenuApp
    {
        public static void LaunchMenuApp(CampusApp campusApp)
        {
            string userInput;

            do
            {
                DisplayGeneralMenu();

                Console.Write("Entrées : ");
                userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    while (userInput != "exit" && userInput != "4")
                    {
                        DisplayStudentMenu();

                        Console.Write("Entrées : ");
                        userInput = Console.ReadLine();

                        ChooseStudentMenu(userInput, campusApp);
                    }
                }
                else if (userInput == "1")
                {
                    while (userInput != "exit" && userInput != "3")
                    {
                        DisplayCourseMenu();

                        Console.Write("Entrées : ");
                        userInput = Console.ReadLine();

                        ChooseCourseMenuOption(userInput, campusApp);
                    }
                }
            }
            while (userInput != "exit");
        }

        public static void DisplayGeneralMenu()
        {
            Console.WriteLine("\nApplication révolutionnaire\n");
            Console.WriteLine("0 : Etudiants");
            Console.WriteLine("1 : Cours");
            Console.WriteLine();
        }

        // Student
        public static void DisplayStudentMenu()
        {
            Console.WriteLine("\nApplication révolutionnaire\n");
            Console.WriteLine("0 : Lister les étudiants");
            Console.WriteLine("1 : Créer un nouvel étudiants");
            Console.WriteLine("2 : Consulter un étudiants existant");
            Console.WriteLine("3 : Ajouter une note et une appréciation pour un cours sur un étudiants existant");
            Console.WriteLine("4 : Revenir au menu principal");
            Console.WriteLine();
        }

        public static void ChooseStudentMenu(string userInput, CampusApp campusApp)
        {
            switch (userInput)
            {
                case "0":
                    campusApp.ListAllStudents();
                    break;
                case "1":
                    campusApp.AddStudent();
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

        // Courses
        public static void DisplayCourseMenu()
        {
            Console.WriteLine("\nApplication révolutionnaire\n");
            Console.WriteLine("0 : Lister les cours existants");
            Console.WriteLine("1 : Ajouter un nouveau cours au programme");
            Console.WriteLine("2 : Supprimer un cours");
            Console.WriteLine("3 : Revenir au menu principal");
            Console.WriteLine();
        }

        public static void ChooseCourseMenuOption(string userInput, CampusApp campusApp)
        {
            switch (userInput)
            {
                case "0":
                    campusApp.ListAllCourses();
                    break;
                case "1":
                    campusApp.AddCourse();
                    break;
                case "2":
                    campusApp.DeleteCourse();
                    break;
                default:
                    break;
            }
        }
    }
}
