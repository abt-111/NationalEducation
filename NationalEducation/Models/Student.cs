using NationalEducation.Interfaces;

namespace NationalEducation.Models
{
    internal class Student : IIdentifiable, IListable
    {
        public uint Id { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Name
        {
            get
            {
                return $"{LastName} - {FirstName}";
            }
        }
        public DateTime DateOfBirth { get; private set; }

        public string Promotion { get; private set; }

        public Student(uint id, string lastName, string firstName, DateTime dateOfBirth, string promotion)
        {
            this.Id = id;
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            Promotion = promotion;
        }

        // Obtenir la liste des notes de l'étudiant
        public List<Grade> GetGradesOfStudent(List<Grade> grades)
        {
            List<Grade> gradesOfStudent = new List<Grade>();

            foreach (Grade grade in grades)
            {
                if (grade.StudentId == Id)
                {
                    gradesOfStudent.Add(grade);
                }
            }

            return gradesOfStudent;
        }

        public static float GetGradesOfStudentAverage(List<Grade> gradesOfStudent)
        {
            return gradesOfStudent.Average(Grade => Grade.Value);
        }
    }
}
