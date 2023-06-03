namespace AttendanceTracker.Data.DataTransferObjects
{
    public class StudentCourseScheduled_DTO
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int CourseScheduledId { get; set; }

        public int CourseId { get; set; }

        public int InstructorId { get; set; }

        public Guid Guid { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public CourseScheduled AsCourseScheduled(Course_DTO course, Instructor_DTO instructor) => new(Guid, course.AsCourse(), instructor.AsInstructor(), StartDate, EndDate);
    }
}
