namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class UpdateCourse : Code_DataRequest
    {
        public UpdateCourse(string courseCode, string? name = null) : base(courseCode) => Name = name;

        public string? Name { get; set; }

        public override object? GetParameters() => new { Code, Name };

        public override string GetSql() => Update.CoalesceTable(TableNames.Course, 
            where: "CourseCode = @Code", 
            columnNamesMatchingParameterNames: "Name");
    }
}
