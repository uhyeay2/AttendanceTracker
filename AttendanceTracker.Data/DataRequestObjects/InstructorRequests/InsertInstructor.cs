namespace AttendanceTracker.Data.DataRequestObjects.InstructorRequests
{
    public class InsertInstructor : IDataRequest
    {
        public InsertInstructor(string instructorCode, string firstName, string lastName)
        {
            InstructorCode = instructorCode;
            FirstName = firstName;
            LastName = lastName;
        }

        public string InstructorCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public object? GetParameters() => new { InstructorCode, FirstName, LastName };

        public string GetSql() => Insert.IntoTable(TableNames.Instructor, "InstructorCode", "FirstName", "LastName");
    }
}
