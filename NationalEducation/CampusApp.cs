using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using NationalEducation.Models;

namespace NationalEducation
{
    internal class CampusApp
    {

        private AppData _appData;

        public CampusApp(AppData appData)
        {
            _appData = appData;
        }

        // Lister les étudiants
        public void ListAllStudents()
        {
            if(_appData.Students.Count > 0)
            {
                Console.WriteLine("Liste des étudiants\n");

                int index = 0;
                foreach (Student student in _appData.Students)
                {
                    Console.WriteLine($"{index} - {student.LastName} - {student.FirstName}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_STUDENTS_ERROR_MESSAGE);
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Créer un nouvel étudiant
        public void CreateNewStudent()
        {
            string lastName;
            string firstName;
            DateTime dateOfBirth;

            // Créer un nouvel étudiant sans vérification de la conformité des données
            Console.WriteLine("Création d'un nouvel étudiant\n");
            
            lastName = InputValidator.GetAndValidNameInput("Entrez un nom : ");
            firstName = InputValidator.GetAndValidNameInput("Entrez un prénom : ");
            dateOfBirth = InputValidator.GetAndValidDateInput("Entrez une date de naissance (format : jj/mm/aaaa) : ");
            _appData.Students.Add(new Student(_appData.StudentsId, lastName, firstName, dateOfBirth));
            _appData.StudentsId++;
        }

        // Sélectionner un étudiant
        public Student SelectStudent()
        {
            int studentIndex;

            if (_appData.Students.Count != 0)
            {
                // On affiche la liste des étudiant et leur index
                ListAllStudents();

                studentIndex = InputValidator.GetAndValidIndexInput("Entrez l'index de l'étudiant pour le sélectionner : ", _appData.Students.Count);

                return _appData.Students[studentIndex];
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_STUDENTS_ERROR_MESSAGE);
            }

            return null;
        }

        // Consulter un élève existant
        public void DisplayStudent()
        {
            Console.WriteLine("Affichage d'un étudiant\n");
            Student student = SelectStudent();

            if(student != null)
            {
                List<Grade> gradesOfStudent = student.GetGradesOfStudent(_appData.Grades);

                Console.WriteLine("Informations sur l'étudiant :\n");
                Console.WriteLine(ConstantValue.INDENT_VALUE, "Nom", student.LastName);
                Console.WriteLine(ConstantValue.INDENT_VALUE, "Prénom", student.FirstName);
                Console.WriteLine(ConstantValue.INDENT_VALUE, "Date de naissance", student.DateOfBirth.ToString("dd/MM/yyyy"));
                Console.WriteLine("\nRésultats scolaires :");

                if(gradesOfStudent.Count > 0)
                {
                    foreach (Grade grade in gradesOfStudent)
                    {
                        Console.WriteLine("\n{0}{1, -7} : {2}", ConstantValue.TABULATION, "Cours",  GetCourseNameWithId(grade.CourseId));
                        Console.WriteLine("{0, -13} : {1}", "\tNote", grade.Value);
                        Console.WriteLine("{0} : {1}", "\tAppréciation", grade.Observation);
                    }

                    float gradesOfStudentAverage = Student.GetGradesOfStudentAverage(gradesOfStudent);
                    Console.WriteLine("\n{0}{1} : {2}", ConstantValue.TABULATION, "Moyenne", gradesOfStudentAverage);
                }
                Console.WriteLine(ConstantValue.SEPARATION);
            }
        }

        // Ajouter une note et une appréciation pour un cours sur un étudiant existant
        public void AddGradeToStudent()
        {
            int courseIndex;
            float gradeValue;
            string observation;

            if (_appData.Courses.Count != 0)
            {
                Console.WriteLine("Ajout de note à un étudiant\n");
                Student student = SelectStudent();

                if (student != null)
                {
                    Console.WriteLine($"Ajout d'une note à {student.LastName} {student.FirstName}\n");

                    // On affiche la liste des cours et leur index
                    ListAllCourses();

                    courseIndex = InputValidator.GetAndValidIndexInput("Entrez l'index du cours pour la note à entrer : ", _appData.Courses.Count);
                    gradeValue = InputValidator.GetAndValidGradeInput("Entrez la note : ");
                    observation = InputValidator.GetAndValidObservationInput("Entrez une appréciation : ");
                    _appData.Grades.Add(new Grade(_appData.GradeId, _appData.Courses[courseIndex].Id, student.Id, gradeValue, observation));
                    _appData.GradeId++;
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_COURSES_ERROR_MESSAGE);
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Lister les cours existants
        public void ListAllCourses()
        {
            if (_appData.Courses.Count > 0)
            {
                Console.WriteLine("Liste des cours\n");

                int index = 0;
                foreach (Course course in _appData.Courses)
                {
                    Console.WriteLine($"{index} - {course.Name}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_COURSES_ERROR_MESSAGE);
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Ajouter un nouveau cours au programme
        public void CreateNewCourse()
        {
            string name;

            // Créer un nouveau cours sans vérification de la conformité des données
            Console.WriteLine("Création d'un nouveau cours\n");
            name = InputValidator.GetAndValidNameInput("Entrez un nom pour le cour : ");
            _appData.Courses.Add(new Course(_appData.CoursesId, name));
            _appData.CoursesId++;

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Supprimer un cours par son identifiant
        // Fonctions DeleteCourseById à créer
        public void DeleteCourse()
        {
            int index;
            string reponse = "";

            if(_appData.Courses.Count > 0)
            {
                // On affiche la liste des cours et leur index
                ListAllCourses();

                index = InputValidator.GetAndValidIndexInput("Entrez l'index du cours à supprimer : ", _appData.Courses.Count);
                Course course = _appData.Courses[index];

                Console.Write($"Vous allez supprimer le cours de {course.Name}. Confirmer O pour Oui et N pour Non : ");
                reponse = Console.ReadLine();

                if(reponse.Equals("O"))
                {
                    _appData.Grades.RemoveAll(x => x.CourseId == course.Id);
                    _appData.Courses.Remove(course);
                }
                else
                {
                    Console.WriteLine("Suppression annulé");
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_COURSES_ERROR_MESSAGE);
            }

            Console.WriteLine(ConstantValue.SEPARATION);
        }

        // Retrouver le nom d'un cours à partir de son identifiant uniquement
        // Revoir la fonction pour qu'elle renvoie carrement l'objet
        public string GetCourseNameWithId(uint id)
        {
            foreach (Course course in _appData.Courses)
            {
                if (course.Id == id)
                {
                    return course.Name;
                }
            }
            return string.Empty;
        }
    }
}
