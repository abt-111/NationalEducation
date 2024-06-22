namespace NationalEducation
{
    internal class Course
    {
        public uint Id { get; }
        public string Name { get; }

        public Course(uint id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
