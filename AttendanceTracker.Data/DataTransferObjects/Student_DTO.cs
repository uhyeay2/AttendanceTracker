namespace AttendanceTracker.Data.DataTransferObjects
{
    public class Student_DTO
    {
        public int Id { get; set; }

        public string StudentCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}
