namespace AttendanceTracker.Data.DataTransferObjects
{
    public class Course_DTO
    {
        public int Id { get; set; }

        public string CourseCode { get; set; } = null!;

        public string Name { get; set; } = null!;

        public Course AsCourse() => new(CourseCode, Name);
    }
}
