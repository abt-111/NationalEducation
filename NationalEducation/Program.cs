using System.Collections.Generic;

namespace NationalEducation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /*Menu
            Au lancement de l'application, un menu permettra à l'utilisateur de choisir entre ces entrées :

            Elèves
            Cours
            Le menu Elèves permettra quant à lui de :

            Lister les élèves
            Créer un nouvel élève
            Consulter un élève existant
            Ajouter une note et une appréciation pour un cours sur un élève existant
            Revenir au menu principal
            Le menu Cours permettra de son côté de :

            Lister les cours existants
            Ajouter un nouveau cours au programme
            Supprimer un cours par son identifiant
            Revenir au menu principal*/

            // Créer un nouvel élève
            List<Student> students = new List<Student>();
            students.Add(new Student(1, "Bomber", "Jean", new DateTime(1993, 08, 06)));
            students.Add(new Student(2, "Tonma", "Yohan", new DateTime(1993, 08, 06)));
            students.Add(new Student(3, "Cherry", "Sandy", new DateTime(1993, 08, 06)));

            // Ajouter un nouveau cours au programme
            List<Course> courses = new List<Course>();
            courses.Add(new Course(1, "Cours1"));
            courses.Add(new Course(2, "Cours2"));
            courses.Add(new Course(3, "Cours3"));

            // Ajouter une note et une appréciation pour un cours sur un élève existant
            students[0].AddGrade(new Grade(courses[0], 12.0f, "Passable"));
            students[0].AddGrade(new Grade(courses[1], 9.99f, "Encore un effort"));



            // Lister les élèves
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.GetID()} | {student.GetLastName()} | {student.GetFirstName()}");
            }

            /*
            ----------------------------------------------------------------------
            Informations sur l'élève : 

            Nom               : Arus
            Prénom            : Joshua
            Date de naissance : 01/01/1980

            Résultats scolaires:

                Cours : Mathématiques
                    Note : 18/20
                    Appréciation : Continue comme ça ! 

                Cours : Anglais
                    Note : 6/20
                    Appréciation : Aie aie aie, ça va pas du tout...

                Cours : Programmation
                    Note : 13/20
                    Appréciation : 

                Moyenne : 12.5/20
             ----------------------------------------------------------------------
             */

            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Informations sur l'élève :\n");
            Console.WriteLine($"Nom               : {students[0].GetLastName()}");
            Console.WriteLine($"Prénom            : {students[0].GetFirstName()}");
            Console.WriteLine($"Date de naissance : {students[0].GetDateOfBirth()}\n");
            Console.WriteLine("Résultats scolaires :\n");
            float meanOfGrades = 0.0f;
            foreach (Grade grade in students[0].GetGrades())
            {
                Console.WriteLine($"    Cours : {grade.GetCourse().GetName()}");
                Console.WriteLine($"        Note : {grade.GetValue()}");
                Console.WriteLine($"        Appréciation : {grade.GetObservation()}\n");
                meanOfGrades += grade.GetValue();
            }
            Console.WriteLine($"   Moyenne : {meanOfGrades / students[0].GetGrades().Count}");
            Console.WriteLine("----------------------------------------------------------------------");
        }
    }
}
