using NationalEducation.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NationalEducation
{
    internal class CourseOperator
    {
        private AppData _appData;

        public CourseOperator(AppData appData)
        {
            _appData = appData;
        }

        public void ListAllCourses() => GenericOperator.ListAll<Course>(_appData.Courses, ConstantValue.COURSES_LIST_DESCRIPTION, ConstantValue.NO_COURSES_LIST_DESCRIPTION);

        // Ajouter un nouveau cours au programme
        public void AddCourse()
        {
            string name;

            Console.WriteLine("Création d'un nouveau cours\n");
            // Saisie de l'utilisateur
            name = InputValidator.GetAndValidNameInput("Entrez un nom pour le cour : ");
            // Ajout d'un nouveau cours dans la list de cours
            _appData.Courses.Add(new Course(GenericOperator.GenerateId<Course>(_appData.Courses), name));

            Log.Information($"Ajout du cours {name}");

            FileOperator.SaveData(_appData);

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Supprimer un cours par son identifiant
        // Fonctions DeleteCourseById à créer
        public void DeleteCourse()
        {
            string reponse = "";

            if (_appData.Courses.Count > 0)
            {
                // Affichage de la liste des cours
                ListAllCourses();
                // Selection d'un cours
                Course course = GenericOperator.Select<Course>(_appData.Courses, ConstantValue.COURSE_SELECT_DESCRIPTION_DELETECOURSE);

                Console.Write($"Vous allez supprimer le cours de {course.Name}. Confirmer O pour Oui et N pour Non : ");
                reponse = Console.ReadLine();

                if (reponse.Equals("O"))
                {
                    _appData.Grades.RemoveAll(x => x.CourseId == course.Id);
                    _appData.Courses.Remove(course);
                    Console.WriteLine("Suppression réussie");

                    Log.Information($"Suppression du cours de {course.Name}");

                    FileOperator.SaveData(_appData);
                }
                else
                {
                    Console.WriteLine("Suppression annulée");
                    Log.Information($"Annulation de la suppression du cours de {course.Name}");
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_COURSES_LIST_DESCRIPTION);
                Log.Information($"Échec de la suppression de cours. {ConstantValue.NO_COURSES_LIST_DESCRIPTION}");
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }
    }
}
