using NationalEducation.Models;

namespace NationalEducation.Operators
{
    internal class PromotionOperator
    {
        private DataApp _appData;

        public PromotionOperator(DataApp appData)
        {
            _appData = appData;
        }

        // Afficher la liste des étudiants d'une promotions
        public void DisplayStudentsInPromotion()
        {
            List<Student> students = SelectPromotionAndGetItsStudents();

            GenericOperator.DisplayItemsOfList(students, ConstantValue.STUDENTS_LIST_DESCRIPTION, ConstantValue.NO_STUDENTS_LIST_DESCRIPTION);
        }

        // Afficher la liste des moyennes par cours d'une promotion donnée
        public void DisplayCoursesAverageForPromotion()
        {
            List<Student> students = SelectPromotionAndGetItsStudents();

            foreach (Course course in _appData.Courses)
            {
                Console.WriteLine($"{course.Name} - {GetCourseAverageForPromotion(students, course)}");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        public List<Student> SelectPromotionAndGetItsStudents()
        {
            // Affichage des promotions avec leur index dans la liste des promotions
            DisplayPromotions(GetPromotions());

            // Sélection de la promotion voulue
            string promotion = GenericOperator.SelectItemOfList(GetPromotions(), ConstantValue.PROMOTION_SELECT_DESCRIPTION);

            // Récupération de la liste des étudiants de la promotion
            return GetStudentsInPromotion(promotion);
        }

        // Afficher la liste des promotions
        public void DisplayPromotions(List<string> promotions)
        {
            if (promotions.Count > 0)
            {
                int index = 0;

                Console.WriteLine($"{ConstantValue.PROMOTIONS_LIST_DESCRIPTION}\n");

                foreach (string promotion in promotions)
                {
                    Console.WriteLine($"{index} - {promotion}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine($"{ConstantValue.NO_PROMOTIONS_LIST_DESCRIPTION}\n");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Récupérer la liste des promotions
        public List<string> GetPromotions()
        {
            List<string> promotions = new List<string>();

            promotions = _appData.Students
                .Select(student => student.Promotion)
                .Distinct() // pour éviter les doublons
                .ToList();

            return promotions;
        }

        // Récupérer la liste des étudiants d'une promotions
        public List<Student> GetStudentsInPromotion(string promotion)
        {
            return _appData.Students.FindAll(student => student.Promotion == promotion);
        }

        // Récupérer la moyenne d'un cours pour une promotion donnée
        public string GetCourseAverageForPromotion(List<Student> students, Course course)
        {
            double moyenne = students
                .SelectMany(student => student.GetGradesOfStudent(_appData.Grades))// SelectMany pour avoir une List<Grade> et pas une List<List<Grade>>
                .Where(grade => grade.CourseId == course.Id)
                .Select(grade => grade.Value)
                .DefaultIfEmpty() // Fournir une valeur par défaut si la séquence est vide
                .Average();

            if (moyenne != 0)
            {
                return $"{moyenne}";
            }
            else
            {
                return $"Pas de moyenne pour cette promotion le moment";
            }
        }
    }
}
