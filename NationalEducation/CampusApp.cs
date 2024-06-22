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
                int index = 0;
                foreach (Student student in Students)
                {
                    Console.WriteLine($"{index} - {student.LastName} - {student.FirstName}");
                    index++;
                }
            }
            else
            {
                Console.WriteLine("Il n'y a pas d'étudiant pour le moment.");
            }
        }

        // Créer un nouvel étudiant
        public void CreateNewStudent()
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
            Students.Add(new Student(_studentsId, lastName, firstName, dateDeNaissance));
            _studentsId++;
        }

        // Consulter un élève existant
        public void DisplayStudent(int index)
        {
            Student student = Students[index];
            List<Grade> gradesOfStudent = GetGradesOfStudent(student);

            // Prototype d'affichage
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Informations sur l'élève :\n");
            Console.WriteLine($"Nom               : {student.LastName}");
            Console.WriteLine($"Prénom            : {student.FirstName}");
            Console.WriteLine($"Date de naissance : {student.DateOfBirth.ToString("dd/MM/yyyy")}\n");
            Console.WriteLine("Résultats scolaires :\n");
            float meanOfGrades = 0.0f;
            foreach (Grade grade in gradesOfStudent)
            {
                Console.WriteLine($"    Cours : {GetCourseNameWithId(grade.CourseId)}");
                Console.WriteLine($"        Note : {grade.Value}");
                Console.WriteLine($"        Appréciation : {grade.Observation}\n");
                meanOfGrades += grade.Value;
            }
            Console.WriteLine($"   Moyenne : {meanOfGrades / gradesOfStudent.Count}");
            Console.WriteLine("----------------------------------------------------------------------");
        }

        // Ajouter une note et une appréciation pour un cours sur un étudiant existant
        // Il faudrait modifier cette méthode pour que ce soit l'index du cours qu'il faille entrer
        public void AddGradeToStudent(Student student)
        {
            uint courseId;
            float gradeValue;
            string observation;

            Console.WriteLine($"Ajout d'une note à {student.LastName} {student.FirstName}\n");
            Console.WriteLine("Liste des cours\n");
            ListAllCourses();

            // On suppose qu'on entre le bon id pour le moment
            Console.Write("Entrez l'identifiant du cours pour la note à entrer : ");
            courseId = UInt32.Parse(Console.ReadLine());
            Console.Write("Entrez la note : ");
            gradeValue = Single.Parse(Console.ReadLine());
            Console.Write("Entrez une appréciation : ");
            observation = Console.ReadLine();
            _grades.Add(new Grade(_gradeId ,courseId, student.Id, gradeValue, observation));
        }

        // Retrouver un étudiant à partir de son identifiant uniquement
        public Student GetStudentByIndex(int index)
        {
            foreach (Student student in Students)
            {
                if (student.Id == index)
                {
                    return student;
                }
            }
            return Students[1];
        }

        // Retrouver un étudiant à partir de son identifiant uniquement
        public List<Grade> GetGradesOfStudent(Student student)
        {
            List<Grade> studentGrades = new List<Grade>();

            foreach (Grade grade in _grades)
            {
                if(grade.StudentId == student.Id)
                {
                    studentGrades.Add(grade);
                }
            }

            return studentGrades;
        }

        // Le menu Cours permettra de son côté de :
        // 
        // Lister les cours existants
        // Ajouter un nouveau cours au programme
        // Supprimer un cours par son identifiant
        // Revenir au menu principal

        // Lister les cours existants
        public void ListAllCourses()
        {
            foreach (Course course in _courses)
            {
                Console.WriteLine($"{course.Id} - {course.Name}");
            }
            Console.WriteLine();
        }

        // Ajouter un nouveau cours au programme
        public void CreateNewCourse()
        {
            string name;

            // Créer un nouveau cours sans vérification de la conformité des données
            Console.WriteLine("Création d'un nouveau cours\n");
            Console.Write("Entrez un nom : ");
            name = Console.ReadLine();
            _courses.Add(new Course(_coursesId, name));
            _coursesId++;
        }

        // Supprimer un cours par son identifiant
        // Fonctions DeleteCourseById à créer

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
