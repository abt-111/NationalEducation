using System.Security.Cryptography.X509Certificates;

namespace NationalEducation
{
    internal class Grade
    {
        private Course _course;
        private float _value;
        private string _observation;

        public Grade(Course course, float value, string observation)
        {
            _course = course;
            _value = value;
            _observation = observation;
        }

        public Course GetCourse() => _course;

        public float GetValue() => _value;

        public string GetObservation() => _observation;
    }
}
