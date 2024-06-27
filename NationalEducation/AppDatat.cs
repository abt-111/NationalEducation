namespace NationalEducation
{
    internal class AppData
    {
        public uint CoursesId { get; set; } = 0;
        public uint StudentsId { get; set; } = 0;
        public uint GradeId { get; set; } = 0;
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Grade> Grades { get; set; } = new List<Grade>();

        public AppData(uint studentId, uint courseId, uint gradeId, List<Student> students, List<Course> courses, List<Grade> grades)
        {
            CoursesId = courseId;
            StudentsId = studentId;
            GradeId = gradeId;
            Courses = courses;
            Students = students;
            Grades = grades;
        }

        public void SaveData()
        {

        }

        public void LoadData()
        {

        }
    }
}
