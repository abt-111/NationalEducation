namespace NationalEducation
{
    internal class Student
    {
        public uint Id { get; }
        public string LastName { get; }
        public string FirstName { get; }
        public DateTime DateOfBirth { get; }

        public Student(uint id, string lastName, string firstName, DateTime dateOfBirth)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
        }

        // Obtenir la liste des notes de l'étudiant
        public List<Grade> GetGradesOfStudent(List<Grade> grades)
        {
            List<Grade> studentGrades = new List<Grade>();

            foreach (Grade grade in grades)
            {
                if (grade.StudentId == Id)
                {
                    studentGrades.Add(grade);
                }
            }

            return studentGrades;
        }
    }
}
