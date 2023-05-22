namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class InsertCourse : IDataRequest
    {
        public InsertCourse(string courseCode, string subjectCode, string name)
        {
            CourseCode = courseCode;
            SubjectCode = subjectCode;
            Name = name;
        }

        public string CourseCode { get; set; }
        public string SubjectCode { get; set; }
        public string Name { get; set; }

        public object? GetParameters() => new { CourseCode, SubjectCode, Name };

        public string GetSql() => Insert.SelectIntoTable(intoTable: TableNames.Course, 
            fromTable: TableNames.Subject, where: "Subject.SubjectCode = @SubjectCode",
            ("SubjectId" , "Subject.Id"), 
            ("CourseCode", "@CourseCode"), 
            ("Name"      , "@Name")
        );
    }
}
