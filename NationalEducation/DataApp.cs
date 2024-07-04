using NationalEducation.Models;

namespace NationalEducation
{
    internal class DataApp
    {
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Grade> Grades { get; set; } = new List<Grade>();

        public DataApp(List<Student> students, List<Course> courses, List<Grade> grades)
        {
            Courses = courses;
            Students = students;
            Grades = grades;
        }
    }
}
