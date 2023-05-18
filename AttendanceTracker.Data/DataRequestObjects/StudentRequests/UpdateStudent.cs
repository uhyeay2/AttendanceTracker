namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class UpdateStudent : StudentCode_DataRequest
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

        public override object? GetParameters() => new { StudentCode, FirstName, LastName, DateOfBirth };

        public override string GetSql() =>
        @"
            UPDATE [dbo].[Student] SET

                FirstName = COALESCE(@FirstName, FirstName),

                LastName = COALESCE(@LastName, LastName),

                DateOfBirth = COALESCE(@DateOfBirth, DateOfBirth)

            WHERE StudentCode = @StudentCode
        ";
    }
}
