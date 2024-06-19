namespace NationalEducation
{
    internal class Student
    {
        private uint _id;
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
        }
    }
}
