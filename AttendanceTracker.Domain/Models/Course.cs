namespace AttendanceTracker.Domain.Models
{
    public class Course
    {
        public Course() { }

        public Course(string courseCode, string name)
        {
            CourseCode = courseCode;
            Name = name;
        }

        public string CourseCode { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
