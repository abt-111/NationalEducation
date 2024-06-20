namespace NationalEducation
{
    internal class Course
    {
        private readonly uint _id;
        private string _name;

        public Course(uint id, string name)
        {
            _id = id;
            _name = name;
        }

        public uint GetId() => _id;

        public string GetName() => _name;
    }
}
