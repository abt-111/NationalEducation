using NationalEducation.Interfaces;

namespace NationalEducation.Models
{
    internal class Student : IIdentifiable
    {
        public uint Id { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public DateTime DateOfBirth { get; }

        public Student(uint id, string lastName, string firstName, DateTime dateOfBirth)
        {
            this.Id = id;
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
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
