using NationalEducation.Interfaces;

namespace NationalEducation.Models
{
    internal class Grade : IIdentifiable
    {
        // Un élève peut avoir plusieurs notes dans un même cours. La note doit donc avoir un identifiant unique.
        public uint Id { get; }
        public uint CourseId { get; }
        public uint StudentId { get; }
        public float Value { get; }
        public string Observation { get; }

        public Grade(uint id, uint courseId, uint studentId, float value, string observation = "")
        {
            Id = id;
            CourseId = courseId;
            StudentId = studentId;
            Value = value;
            Observation = observation;
        }
    }
}
