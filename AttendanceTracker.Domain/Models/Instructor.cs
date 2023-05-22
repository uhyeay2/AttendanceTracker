namespace AttendanceTracker.Domain.Models
{
    public class Instructor
    {
        public Instructor() { }

        public Instructor(string instructorCode, string firstName, string lastName)
        {
            InstructorCode = instructorCode;
            FirstName = firstName;
            LastName = lastName;
        }

        public string InstructorCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
