namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class UpdateStudent : Code_DataRequest
    {
        public UpdateStudent(string studentCode, string? firstName = null, string? lastName = null, DateTime? dateOfBirth = null) : base(studentCode)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public override object? GetParameters() => new { Code, FirstName, LastName, DateOfBirth };

        public override string GetSql() => Update.CoalesceTable(TableNames.Student,
            where: "StudentCode = @Code",
            "FirstName", "LastName", "DateOfBirth");
    }
}
