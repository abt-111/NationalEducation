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
            students.Add(new Student(1, "A", "B", new DateTime(1993, 08, 06)));
            students.Add(new Student(2, "C", "D", new DateTime(1993, 08, 06)));
            students.Add(new Student(3, "E", "F", new DateTime(1993, 08, 06)));

            // Ajouter un nouveau cours au programme
            List<Course> courses = new List<Course>();
            courses.Add(new Course(1, "A"));
            courses.Add(new Course(2, "B"));
            courses.Add(new Course(3, "C"));
        }
    }
}
