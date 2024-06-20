namespace NationalEducation
{
    internal class Grade
    {
        private readonly uint _courseId;
        private float _value;
        private string _observation;

        public Grade(uint courseId, float value)
        {
            _courseId = courseId;
            _value = value;
            _observation = "";
        }
        public Grade(uint courseId, float value, string observation)
        {
            _courseId = courseId;
            _value = value;
            _observation = observation;
        }

        public uint GetCourseId() => _courseId;

        public float GetValue() => _value;

        public string GetObservation() => _observation;
    }
}
