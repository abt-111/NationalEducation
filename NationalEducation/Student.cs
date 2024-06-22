namespace NationalEducation
{
    internal class Student
    {
        private readonly uint _id;
        private string _lastName;
        private string _firstName;
        private DateTime _dateOfBirth;

        public Student(uint id, string lastName, string firstName, DateTime dateOfBirth)
        {
            _id = id;
            _lastName = lastName;
            _firstName = firstName;
            _dateOfBirth = dateOfBirth;
        }

        public uint GetId() => _id;
        public string GetLastName() => _lastName;
        public string GetFirstName() => _firstName;
        public string GetDateOfBirth()
        {
            return _dateOfBirth.ToString("dd/MM/yyyy");
        }
    }
}
