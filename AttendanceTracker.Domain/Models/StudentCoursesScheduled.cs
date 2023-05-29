namespace AttendanceTracker.Domain.Models
{
    public class StudentCoursesScheduled
    {
        public StudentCoursesScheduled() { }

        public StudentCoursesScheduled(Student student, IEnumerable<CourseScheduled> courseScheduled)
        {
            Student = student;
            CoursesScheduled = courseScheduled;
        }

        public Student Student { get; set; } = null!;

        public IEnumerable<CourseScheduled> CoursesScheduled { get; set; } = Enumerable.Empty<CourseScheduled>();
    }
}
