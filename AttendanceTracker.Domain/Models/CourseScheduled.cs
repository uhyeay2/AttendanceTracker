namespace AttendanceTracker.Domain.Models
{
    public class CourseScheduled
    {
        public CourseScheduled() { }

        public CourseScheduled(Guid guid, Course course, Instructor instructor, DateTime startDate, DateTime endDate)
        {
            Guid = guid;
            Course = course;
            Instructor = instructor;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid Guid { get; set; }

        public Course Course { get; set; } = null!;

        public Instructor Instructor { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
