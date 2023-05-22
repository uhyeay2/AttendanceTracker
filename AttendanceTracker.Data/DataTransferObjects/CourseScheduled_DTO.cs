namespace AttendanceTracker.Data.DataTransferObjects
{
    public class CourseScheduled_DTO
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }
        
        public int CourseId { get; set; }
        
        public int InstructorId { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
