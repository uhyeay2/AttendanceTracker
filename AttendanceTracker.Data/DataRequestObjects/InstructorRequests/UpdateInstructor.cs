namespace AttendanceTracker.Data.DataRequestObjects.InstructorRequests
{
    public class UpdateInstructor : Code_DataRequest
    {
        public UpdateInstructor(string code, string? firstName = null, string? lastName = null) : base(code)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;

        public override object? GetParameters() => new { Code, FirstName, LastName };

        public override string GetSql() => 
            Update.CoalesceTable(TableNames.Instructor, where: "InstructorCode = @Code", 
                "FirstName", "LastName");
    }
}
