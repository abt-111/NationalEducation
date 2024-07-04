using NationalEducation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalEducation
{
    internal class PromotionOperator
    {
        private AppData _appData;

        public PromotionOperator(AppData appData)
        {
            _appData = appData;
        }

        // Récupérer la liste des promotions
        public List<string> GetPromotions()
        {
            List<string> promotions = new List<string>();

            foreach (Student student in _appData.Students)
            {
                if (!promotions.Contains(student.Promotion))
                {
                    promotions.Add(student.Promotion);
                }
            }

            return promotions;
        }

        // Afficher la liste des promotions
        public void ListAllPromotions(List<string> promotions)
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

        // Récupérer la liste des étudiants d'une promotions
        public List<Student> GetAllStudentsOfPromotions(string promotion)
        {
            return _appData.Students.FindAll(x => x.Promotion == promotion);
        }

        // Afficher la liste des étudiants d'une promotions
        public void ListAllStudentsOfPromotion()
        {
            ListAllPromotions(GetPromotions());

            String promotion = GenericOperator.Select<string>(GetPromotions(), ConstantValue.PROMOTION_SELECT_DESCRIPTION);

            List<Student> students = GetAllStudentsOfPromotions(promotion);

            GenericOperator.ListAll<Student>(students, ConstantValue.STUDENTS_LIST_DESCRIPTION, ConstantValue.NO_STUDENTS_LIST_DESCRIPTION);

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Afficher la liste des moyennes par cours d'une promotion donnée
        public void ListAllAverageOfCoursesOfPromotion()
        {
            ListAllPromotions(GetPromotions());

            String promotion = GenericOperator.Select<string>(GetPromotions(), ConstantValue.PROMOTION_SELECT_DESCRIPTION);

            List<Student> students = _appData.Students.FindAll(x => x.Promotion == promotion);

            foreach (Course course in _appData.Courses)
            {
                Console.WriteLine($"{course.Name} - {GetAverageOfCourseOfPromotion(students, course)}");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Récupérer la moyenne d'un cours pour une promotion donnée
        public string GetAverageOfCourseOfPromotion(List<Student> students, Course course)
        {
            float sum = 0;
            int counter = 0;
            double moyenne = 0;

            foreach (Student student in students)
            {
                foreach (Grade grade in student.GetGradesOfStudent(_appData.Grades))
                {
                    if (grade.CourseId == course.Id)
                    {
                        sum += grade.Value;
                        counter++;
                    }
                }
            }

            if (counter != 0)
            {
                moyenne = Math.Round((sum / counter), 1);

                return $"{moyenne}";
            }
            else
            {
                return $"Pas de moyenne pour cette promotion le moment";
            }
        }
    }
}
