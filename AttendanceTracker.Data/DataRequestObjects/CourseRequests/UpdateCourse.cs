namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    internal class UpdateCourse : CourseCode_DataRequest
    {
        public UpdateCourse(string courseCode) : base(courseCode) { }

        public override string GetSql() => Update.CoalesceTable(TableNames.Course, 
            where: "CourseCode = @CourseCode", 
            columnNamesMatchingParameterNames: "Name");
    }
}
