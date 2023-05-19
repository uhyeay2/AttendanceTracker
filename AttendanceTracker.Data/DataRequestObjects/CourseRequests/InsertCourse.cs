namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class InsertCourse : IDataRequest
    {
        public InsertCourse(string courseCode, string name)
        {
            CourseCode = courseCode;
            Name = name;
        }

        public string CourseCode { get; set; }

        public string Name { get; set; }

        public object? GetParameters() => new { Name, CourseCode }; 

        public string GetSql() => Insert.IntoTable(TableNames.Course, "Name", "CourseCode");
    }
}
