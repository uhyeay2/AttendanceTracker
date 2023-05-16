namespace AttendanceTracker.Domain.Models
{
    public class CourseScheduled : Course
    {
        public Instructor Instructor { get; set; } = null!;

        public DateTime BeginDateTimeUTC { get; set; }

        public DateTime EndDateTimeUTC { get; set; }
    }
}
