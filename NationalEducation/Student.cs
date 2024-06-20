namespace NationalEducation
{
    internal class Student
    {
        private readonly uint _id;
        private string _lastName;
        private string _firstName;
        private DateTime _dateOfBirth;
        private List<Grade> _grades;

        public Student(uint id, string lastName, string firstName, DateTime dateOfBirth)
        {
            _id = id;
            _lastName = lastName;
            _firstName = firstName;
            _dateOfBirth = dateOfBirth;
            _grades = new List<Grade>();
        }

        public uint GetId() => _id;
        public string GetLastName() => _lastName;
        public string GetFirstName() => _firstName;
        public string GetDateOfBirth()
        {
            return _dateOfBirth.ToString("dd/MM/yyyy");
        }

        public List<Grade> GetGrades()
        {
            return _grades;
        }

        public void AddGrade(Grade grade)
        {
            _grades.Add(grade);
        }
    }
}
