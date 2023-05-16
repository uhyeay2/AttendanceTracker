namespace AttendanceTracker.Domain.Models
{
    public class Student
    {
        public Student() { }

        public Student(string studentCode, string firstName, string lastName, DateTime dateOfBirth)
        {
            StudentCode = studentCode;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public string StudentCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}
