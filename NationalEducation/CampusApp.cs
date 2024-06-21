namespace NationalEducation
{
    internal class CampusApp
    {
        private uint _studentsId = 0;
        private uint _coursesId = 0;
        private List<Student> _students = new List<Student>();
        private List<Course> _courses = new List<Course>();
        private List<Grade> _grades = new List<Grade>();

        public CampusApp(uint studentId, uint courseId, List<Student> students, List<Course> courses, List<Grade> grades)
        {
            _studentsId = studentId;
            _coursesId = courseId;
            _students = students;
            _courses = courses;
            _grades = grades;
        }

        public uint StudentsId
        {
            get => _studentsId;
            set => _studentsId = value;
        }

        public uint CoursesId
        {
            get => _coursesId;
            set => _coursesId = value;
        }

        public List<Student> Students
        {
            get => _students;
            set => _students = value;
        }

        public List<Course> Courses
        {
            get => _courses;
            set => _courses = value;
        }

        // Le menu Elèves permettra de :
        // 
        // Lister les étudiants
        // Créer un nouvel étudiant
        // Consulter un étudiant existant
        // Ajouter une note et une appréciation pour un cours sur un étudiant existant
        // Revenir au menu principal

        // Lister les étudiants
        public void ListAllStudents()
        {
            if( _students.Count > 0)
            {
                int index = 0;
                foreach (Student student in _students)
                {
                    Console.WriteLine($"{index} - {student.GetLastName()} - {student.GetFirstName()}");
                    Console.WriteLine(_students[index].GetLastName());
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
            _students.Add(new Student(_studentsId, lastName, firstName, dateDeNaissance));
            _studentsId++;
        }

        // Consulter un élève existant
        public void DisplayStudent(int index)
        {
            Student student = _students[index];
            List<Grade> gradesOfStudent = GetGradesOfStudent(student);

            // Prototype d'affichage
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("Informations sur l'élève :\n");
            Console.WriteLine($"Nom               : {student.GetLastName()}");
            Console.WriteLine($"Prénom            : {student.GetFirstName()}");
            Console.WriteLine($"Date de naissance : {student.GetDateOfBirth()}\n");
            Console.WriteLine("Résultats scolaires :\n");
            float meanOfGrades = 0.0f;
            foreach (Grade grade in gradesOfStudent)
            {
                Console.WriteLine($"    Cours : {GetCourseNameWithId(grade.GetCourseId())}");
                Console.WriteLine($"        Note : {grade.GetValue()}");
                Console.WriteLine($"        Appréciation : {grade.GetObservation()}\n");
                meanOfGrades += grade.GetValue();
            }
            Console.WriteLine($"   Moyenne : {meanOfGrades / gradesOfStudent.Count}");
            Console.WriteLine("----------------------------------------------------------------------");
        }

        // Ajouter une note et une appréciation pour un cours sur un étudiant existant
        public void AddGradeToStudent(Student student)
        {
            uint courseId;
            float gradeValue;
            string observation;

            Console.WriteLine($"Ajout d'une note à {student.GetLastName()} {student.GetFirstName()}\n");
            Console.WriteLine("Liste des cours\n");
            ListAllCourses();

            // On suppose qu'on entre le bon id pour le moment
            Console.Write("Entrez l'identifiant du cours pour la note à entrer : ");
            courseId = UInt32.Parse(Console.ReadLine());
            Console.Write("Entrez la note : ");
            gradeValue = Single.Parse(Console.ReadLine());
            Console.Write("Entrez une appréciation : ");
            observation = Console.ReadLine();
            _grades.Add(new Grade(courseId, student.GetId(), gradeValue, observation));
        }

        // Retrouver un étudiant à partir de son identifiant uniquement
        public Student GetStudentByIndex(int index)
        {
            foreach (Student student in _students)
            {
                if (student.GetId() == index)
                {
                    return student;
                }
            }
            return _students[1];
        }

        // Retrouver un étudiant à partir de son identifiant uniquement
        public List<Grade> GetGradesOfStudent(Student student)
        {
            List<Grade> studentGrades = new List<Grade>();

            foreach (Grade grade in _grades)
            {
                if(grade.GetStudentId() == student.GetId())
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
                Console.WriteLine($"{course.GetId()} - {course.GetName()}");
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
                if (course.GetId() == id)
                {
                    return course.GetName();
                }
            }
            return string.Empty;
        }
    }
}
