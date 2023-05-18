namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class InsertStudent : IDataRequest
    {
        public InsertStudent(string studentCode, string firstName, string lastName, DateTime dateOfBirth)
        {
            StudentCode = studentCode;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public string StudentCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public object? GetParameters() => new { StudentCode, FirstName, LastName, DateOfBirth };

        public string GetSql() => Insert.IntoTable(TableNames.Student, "StudentCode", "FirstName", "LastName", "DateOfBirth");
    }
}
