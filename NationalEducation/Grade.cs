namespace NationalEducation
{
    internal class Grade
    {
        private readonly uint _courseId;
        private readonly uint _studentId;
        private float _value;
        private string _observation;

        public Grade(uint courseId, uint studentId, float value, string observation = "")
        {
            _courseId = courseId;
            _studentId = studentId;
            _value = value;
            _observation = observation;
        }

        public uint GetCourseId() => _courseId;

        public uint GetStudentId() => _studentId;

        public float GetValue() => _value;

        public string GetObservation() => _observation;
    }
}
