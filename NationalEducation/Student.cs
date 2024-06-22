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
    }
}
