using System.Collections.Generic;

namespace NationalEducation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CampusApp campusApp = new CampusApp(0, 0, new List<Student>(), new List<Course>());

            // Créer un nouvel élève
            campusApp.CreateNewStudent();
            campusApp.CreateNewStudent();

            // Lister les élèves
            campusApp.ListAllStudents();

            // Ajouter un nouveau cours au programme
            campusApp.CreateNewCourse();
            campusApp.CreateNewCourse();

            // Lister les élèves
            campusApp.ListAllStudents();

            // Ajouter une note et une appréciation pour un cours sur un élève existant
            campusApp.AddGradeToStudent(campusApp.Students[0]);
            campusApp.AddGradeToStudent(campusApp.Students[0]);

            // Afficher un étudiant en particulier
            campusApp.DisplayStudent(0);
        }
    }
}
