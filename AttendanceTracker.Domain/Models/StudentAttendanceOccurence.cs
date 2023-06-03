namespace AttendanceTracker.Domain.Models
{
    public class StudentAttendanceOccurence
    {
        public StudentAttendanceOccurence() { }

        public StudentAttendanceOccurence(Student student, CourseScheduled courseScheduled, AttendanceOccurence attendanceOccurence)
        {
            Student = student;
            CourseScheduled = courseScheduled;
            AttendanceOccurence = attendanceOccurence;
        }

        public Student Student { get; set; } = null!;

        public CourseScheduled CourseScheduled { get; set; } = null!;

        public AttendanceOccurence AttendanceOccurence { get; set; } = null!;
    }
}
