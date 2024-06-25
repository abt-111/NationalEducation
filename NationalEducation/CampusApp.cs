using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NationalEducation
{
    internal class CampusApp
    {
        private uint _coursesId = 0;
        private uint _studentsId = 0;
        private uint _gradeId = 0;
        private List<Course> _courses = new List<Course>();
        public List<Student> Students { get; } = new List<Student>();
        private List<Grade> _grades = new List<Grade>();

        public CampusApp(uint studentId, uint courseId, uint gradeId, List<Student> students, List<Course> courses, List<Grade> grades)
        {
            _coursesId = courseId;
            _studentsId = studentId;
            _gradeId = gradeId; 
            _courses = courses;
            Students = students;
            _grades = grades;
        }

        // Lister les étudiants
        public void ListAllStudents()
        {
            if( Students.Count > 0)
            {
                Console.WriteLine("Liste des étudiants\n");

                int index = 0;
                foreach (Student student in Students)
                {
                    Console.WriteLine($"{index} - {student.LastName} - {student.FirstName}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_STUDENTS_ERROR_MESSAGE);
            }

            Console.WriteLine();
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
            Students.Add(new Student(_studentsId, lastName, firstName, dateOfBirth));
            _studentsId++;
        }

        // Sélectionner un étudiant
        public Student SelectStudent()
        {
            int studentIndex;

            if (Students.Count != 0)
            {
                // On affiche la liste des étudiant et leur index
                ListAllStudents();

                studentIndex = InputValidator.GetAndValidIndexInput("Entrez l'index de l'étudiant pour le sélectionner : ", Students.Count);

                return Students[studentIndex];
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
                List<Grade> gradesOfStudent = student.GetGradesOfStudent(_grades);

                // Prototype d'affichage
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Informations sur l'élève :\n");
                Console.WriteLine($"Nom               : {student.LastName}");
                Console.WriteLine($"Prénom            : {student.FirstName}");
                Console.WriteLine($"Date de naissance : {student.DateOfBirth.ToString("dd/MM/yyyy")}\n");
                Console.WriteLine("Résultats scolaires :\n");

                if(gradesOfStudent.Count > 0)
                {
                    float meanOfGrades = 0.0f;

                    foreach (Grade grade in gradesOfStudent)
                    {
                        Console.WriteLine($"    Cours : {GetCourseNameWithId(grade.CourseId)}");
                        Console.WriteLine($"        Note : {grade.Value}");
                        Console.WriteLine($"        Appréciation : {grade.Observation}\n");
                        meanOfGrades += grade.Value;
                    }
                    Console.WriteLine($"   Moyenne : {meanOfGrades / gradesOfStudent.Count}");
                }
                Console.WriteLine("----------------------------------------------------------------------");
            }
        }

        // Ajouter une note et une appréciation pour un cours sur un étudiant existant
        public void AddGradeToStudent()
        {
            int courseIndex;
            float gradeValue;
            string observation;

            if (_courses.Count != 0)
            {
                Console.WriteLine("Ajout de note à un étudiant\n");
                Student student = SelectStudent();

                if (student != null)
                {
                    Console.WriteLine($"Ajout d'une note à {student.LastName} {student.FirstName}\n");

                    // On affiche la liste des cours et leur index
                    ListAllCourses();

                    courseIndex = InputValidator.GetAndValidIndexInput("Entrez l'index du cours pour la note à entrer : ", _courses.Count);
                    gradeValue = InputValidator.GetAndValidGradeInput("Entrez la note : ");
                    observation = InputValidator.GetAndValidObservationInput("Entrez une appréciation : ");
                    _grades.Add(new Grade(_gradeId, _courses[courseIndex].Id, student.Id, gradeValue, observation));
                    _gradeId++;
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_COURSES_ERROR_MESSAGE);
            }
        }

        // Lister les cours existants
        public void ListAllCourses()
        {
            if (_courses.Count > 0)
            {
                Console.WriteLine("Liste des cours\n");

                int index = 0;
                foreach (Course course in _courses)
                {
                    Console.WriteLine($"{index} - {course.Name}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine(ConstantValue.NO_COURSES_ERROR_MESSAGE);
            }
            
            Console.WriteLine();
        }

        // Ajouter un nouveau cours au programme
        public void CreateNewCourse()
        {
            string name;

            // Créer un nouveau cours sans vérification de la conformité des données
            Console.WriteLine("Création d'un nouveau cours\n");
            name = InputValidator.GetAndValidNameInput("Entrez un nom pour le cour : ");
            _courses.Add(new Course(_coursesId, name));
            _coursesId++;
        }

        // Supprimer un cours par son identifiant
        // Fonctions DeleteCourseById à créer
        public void DeleteCourse()
        {
            int index;
            string reponse = "";

            if(_courses.Count > 0)
            {
                // On affiche la liste des cours et leur index
                ListAllCourses();

                index = InputValidator.GetAndValidIndexInput("Entrez l'index du cours à supprimer : ", _courses.Count);
                Course course = _courses[index];

                Console.Write("Vous allez supprimer le cours de {course.Name}. Confirmer O pour Oui et N pour Non : ");
                reponse = Console.ReadLine();

                if(reponse.Equals("O"))
                {
                    _grades.RemoveAll(x => x.CourseId == course.Id);
                    _courses.Remove(course);
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
        }

        // Retrouver le nom d'un cours à partir de son identifiant uniquement
        // Revoir la fonction pour qu'elle renvoie carrement l'objet
        public string GetCourseNameWithId(uint id)
        {
            foreach (Course course in _courses)
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
