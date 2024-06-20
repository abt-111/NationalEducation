using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

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

            Consulter un élève existant
            Revenir au menu principal
            Le menu Cours permettra de son côté de :

            Supprimer un cours par son identifiant
            Revenir au menu principal*/

            // Voir génération d'identifiant unique par la suite (?)
            uint studentsId = 0;
            uint coursesId = 0;
            List<Student> students = new List<Student>();
            List<Course> courses = new List<Course>();

            // Créer un nouvel élève            
            CreateNewStudent(students, ref studentsId);
            CreateNewStudent(students, ref studentsId);

            // Ajouter un nouveau cours au programme
            CreateNewCourse(courses, ref coursesId);
            CreateNewCourse(courses, ref coursesId);

            // Lister les élèves
            ListAllStudents(students);

            // Ajouter une note et une appréciation pour un cours sur un élève existant
            AddGradeToStudent(students[0], courses);
            AddGradeToStudent(students[0], courses);

            DisplayStudent(students, courses, 0);
        }

        // Créer un nouvel étudiant
        public static void CreateNewStudent(List<Student> students, ref uint id)
        {
            string lastName;
            string firstName;
            string dateOfBirth;

            // Créer un nouvel étudiant sans vérification de la conformité des données
            Console.WriteLine("Création d'un nouvel étudiant\n");
            Console.Write("Entrez un nom : ");
            lastName = Console.ReadLine();
            Console.Write("Entrez un prénom : ");
            firstName = Console.ReadLine();
            Console.Write("Entrez une date de naissance (format : jj/mm/yyyy) : ");
            dateOfBirth = Console.ReadLine();
            DateTime dateDeNaissance = DateTime.Parse(dateOfBirth);
            students.Add(new Student(id, lastName, firstName, dateDeNaissance));
            id++;
        }

        // Ajouter un nouveau cours au programme
        public static void CreateNewCourse(List<Course> courses, ref uint id)
        {
            string name;

            // Créer un nouveau cours sans vérification de la conformité des données
            Console.WriteLine("Création d'un nouveau cours\n");
            Console.Write("Entrez un nom : ");
            name = Console.ReadLine();
            courses.Add(new Course(id, name));
            id++;
        }

        // Ajouter une note et une appréciation pour un cours sur un élève existant
        public static void AddGradeToStudent(Student student, List<Course> courses)
        {
            uint courseId;
            float gradeValue;
            string observation;

            Console.WriteLine($"Ajout d'une note à {student.GetLastName()} {student.GetFirstName()}\n");
            Console.WriteLine("Liste des cours\n");
            ListAllCourses(courses);

            // On suppose qu'on entre le bon id pour le moment
            Console.Write("Entrez l'identifiant du cours pour la note à entrer : ");
            courseId = UInt32.Parse(Console.ReadLine());
            Console.Write("Entrez la note : ");
            gradeValue = Single.Parse(Console.ReadLine());
            Console.Write("Entrez une appréciation : ");
            observation = Console.ReadLine();

            if(observation == "")
            {
                student.AddGrade(new Grade(courseId, gradeValue));
            }
            else
            {
                student.AddGrade(new Grade(courseId, gradeValue, observation));
            }
        }

        // Lister les cours existants
        public static void ListAllCourses(List<Course> courses)
        {
            foreach (Course course in courses)
            {
                Console.WriteLine($"{course.GetId()} - {course.GetName()}");
            }
            Console.WriteLine();
        }

        // Lister les étudiants
        public static void ListAllStudents(List<Student> students)
        {
            foreach (Student student in students)
            {
                Console.WriteLine($"{student.GetId()} - {student.GetLastName()} - {student.GetFirstName()}");
            }
        }

        // Retrouver le nom d'un cours à partir de son id uniquement
        public static string GetCourseNameWithId(List<Course> courses, uint id)
        {
            foreach (Course course in courses)
            {
                if(course.GetId() == id)
                {
                    return course.GetName();
                }
            }
            return string.Empty;
        }

        // Consulter un élève existant
        public static void DisplayStudent(List<Student> students, List<Course> courses, uint id)
        {
            Student student = GetStudentById(students, id);

            // Prototype d'affichage
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Informations sur l'élève :\n");
            Console.WriteLine($"Nom               : {student.GetLastName()}");
            Console.WriteLine($"Prénom            : {student.GetFirstName()}");
            Console.WriteLine($"Date de naissance : {student.GetDateOfBirth()}\n");
            Console.WriteLine("Résultats scolaires :\n");
            float meanOfGrades = 0.0f;
            foreach (Grade grade in student.GetGrades())
            {
                Console.WriteLine($"    Cours : {GetCourseNameWithId(courses, grade.GetCourseId())}");
                Console.WriteLine($"        Note : {grade.GetValue()}");
                Console.WriteLine($"        Appréciation : {grade.GetObservation()}\n");
                meanOfGrades += grade.GetValue();
            }
            Console.WriteLine($"   Moyenne : {meanOfGrades / student.GetGrades().Count}");
            Console.WriteLine("----------------------------------------------------------------------");
        }
        public static Student GetStudentById(List<Student> students, uint id)
        {
            foreach (Student student in students)
            {
                if (student.GetId() == id)
                {
                    return student;
                }
            }
            return students[1];
        }
    }
}
