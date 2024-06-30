using NationalEducation.Interfaces;

namespace NationalEducation.Models
{
    internal class Course : IIdentifiable, IListable
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
