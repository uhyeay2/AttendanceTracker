namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    internal class UpdateCourse : CourseCode_DataRequest
    {
        public UpdateCourse(string courseCode, string? name = null) : base(courseCode) => Name = name;

        public string? Name { get; set; }

        public override object? GetParameters() => new { CourseCode, Name };

        public override string GetSql() => Update.CoalesceTable(TableNames.Course, 
            where: "CourseCode = @CourseCode", 
            columnNamesMatchingParameterNames: "Name");
    }
}
