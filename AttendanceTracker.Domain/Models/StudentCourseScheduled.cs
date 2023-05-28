namespace AttendanceTracker.Domain.Models
{
    public class StudentCourseScheduled
    {
        public StudentCourseScheduled() { }

        public StudentCourseScheduled(Student student, CourseScheduled courseScheduled)
        {
            Student = student;
            CourseScheduled = courseScheduled;
        }

        public Student Student { get; set; } = null!;

        public CourseScheduled CourseScheduled { get; set; } = null!;
    }
}
