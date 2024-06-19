namespace NationalEducation
{
    internal class Course
    {
        private uint _id;
        private string _name;

        public Course(uint id, string name)
        {
            _id = id;
            _name = name;
        }

        public string GetName() => _name;
    }
}
