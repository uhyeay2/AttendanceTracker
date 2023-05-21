namespace AttendanceTracker.Domain.Models
{
    public class Subject
    {
        public Subject() { }

        public Subject(string subjectCode, string name)
        {
            SubjectCode = subjectCode;
            Name = name;
        }

        public string SubjectCode { get; set; } = null!;

        public string Name { get; set; } = null!;
    }
}
