using Serilog;

namespace NationalEducation
{
    internal static class MenuApp
    {
        // General
        public static void GeneralMenuLoop(CampusApp campusApp)
        {
            string userInput;

            do
            {
                DisplayGeneralMenu();

                Console.Write("Entrées : ");
                userInput = Console.ReadLine();

                if (userInput == "0")
                {
                    StudentMenuLoop(userInput, campusApp);
                }
                else if (userInput == "1")
                {
                    CourseMenuLoop(userInput, campusApp);
                }
                else if (userInput == "2")
                {
                    PromotionMenuLoop(userInput, campusApp);
                }
            }
            while (userInput != "-1");
        }

        public static void DisplayGeneralMenu()
        {
            Console.Clear();
            Console.WriteLine("National Education Application\n");
            Console.WriteLine("-1 : Quitter");
            Console.WriteLine(" 0 : Etudiants");
            Console.WriteLine(" 1 : Cours");
            Console.WriteLine(" 2 : Promotions");
            Console.WriteLine();

            Log.Information("Affichage du menu general");
        }

        // Student
        public static void StudentMenuLoop(string userInput, CampusApp campusApp)
        {
            while (userInput != "-1" && userInput != "4")
            {
                DisplayStudentMenu();

                Console.Write("Entrées : ");
                userInput = Console.ReadLine();

                ChooseStudentMenu(userInput, campusApp);
            }

            Log.Information("Retour au menu principal");
        }

        public static void DisplayStudentMenu()
        {
            Console.Clear();
            Console.WriteLine("National Education Application\n");
            Console.WriteLine("0 : Lister les étudiants");
            Console.WriteLine("1 : Créer un nouvel étudiants");
            Console.WriteLine("2 : Consulter un étudiants existant");
            Console.WriteLine("3 : Ajouter une note et une appréciation pour un cours sur un étudiants existant");
            Console.WriteLine("4 : Revenir au menu principal");
            Console.WriteLine();

            Log.Information("Affichage du menu student");
        }

        public static void ChooseStudentMenu(string userInput, CampusApp campusApp)
        {
            switch (userInput)
            {
                case "0":
                    Console.Clear();
                    campusApp.StudentOperator.ListAllStudents();
                    AskForKeyPress();
                    break;
                case "1":
                    Console.Clear();
                    campusApp.StudentOperator.AddStudent();
                    AskForKeyPress();
                    break;
                case "2":
                    Console.Clear();
                    campusApp.StudentOperator.DisplayStudent();
                    AskForKeyPress();
                    break;
                case "3":
                    Console.Clear();
                    campusApp.AddGradeToStudent();
                    AskForKeyPress();
                    break;
                default:
                    break;
            }
        }

        // Courses
        public static void CourseMenuLoop(string userInput, CampusApp campusApp)
        {
            while (userInput != "-1" && userInput != "3")
            {
                DisplayCourseMenu();

                Console.Write("Entrées : ");
                userInput = Console.ReadLine();

                ChooseCourseMenuOption(userInput, campusApp);
            }

            Log.Information("Retour au menu principal");
        }

        public static void DisplayCourseMenu()
        {
            Console.Clear();
            Console.WriteLine("National Education Application\n");
            Console.WriteLine("0 : Lister les cours existants");
            Console.WriteLine("1 : Ajouter un nouveau cours au programme");
            Console.WriteLine("2 : Supprimer un cours");
            Console.WriteLine("3 : Revenir au menu principal");
            Console.WriteLine();

            Log.Information("Affichage du menu course");
        }

        public static void ChooseCourseMenuOption(string userInput, CampusApp campusApp)
        {
            switch (userInput)
            {
                case "0":
                    Console.Clear();
                    campusApp.ListAllCourses(campusApp.PromotionOperator.GetPromotions());
                    AskForKeyPress();
                    break;
                case "1":
                    Console.Clear();
                    campusApp.CourseOperator.AddCourse();
                    AskForKeyPress();
                    break;
                case "2":
                    Console.Clear();
                    campusApp.CourseOperator.DeleteCourse();
                    AskForKeyPress();
                    break;
                default:
                    break;
            }
        }

        // Promotions
        public static void PromotionMenuLoop(string userInput, CampusApp campusApp)
        {
            while (userInput != "-1" && !userInput.Equals("3"))
            {
                DisplayPromotionMenu();

                Console.Write("Entrées : ");
                userInput = Console.ReadLine();

                ChoosePromotionMenuOption(userInput, campusApp);
            }

            Log.Information("Retour au menu principal");
        }

        public static void DisplayPromotionMenu()
        {
            Console.Clear();
            Console.WriteLine("National Education Application\n");
            Console.WriteLine("0 : Lister les promotions");
            Console.WriteLine("1 : Lister les élèves d'une promotion");
            Console.WriteLine("2 : Afficher la moyenne par cours de tous les élèves d'une promotion donnée");
            Console.WriteLine("3 : Revenir au menu principal");
            Console.WriteLine();
        }

        public static void ChoosePromotionMenuOption(string userInput, CampusApp campusApp)
        {
            switch (userInput)
            {
                case "0":
                    Console.Clear();
                    campusApp.PromotionOperator.ListAllPromotions(campusApp.PromotionOperator.GetPromotions());
                    AskForKeyPress();
                    break;
                case "1":
                    Console.Clear();
                    campusApp.PromotionOperator.ListAllStudentsOfPromotion();
                    AskForKeyPress();
                    break;
                case "2":
                    Console.Clear();
                    campusApp.PromotionOperator.ListAllAverageOfCoursesOfPromotion();
                    AskForKeyPress();
                    break;
                default:
                    break;
            }
        }

        public static void AskForKeyPress()
        {
            Console.Write("Appuyer sur une touche pour continuer");
            Console.ReadKey();
        }
    }
}
