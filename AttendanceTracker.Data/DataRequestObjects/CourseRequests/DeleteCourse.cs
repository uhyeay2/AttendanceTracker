namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class DeleteCourse : CourseCode_DataRequest
    {
        public DeleteCourse(string courseCode) : base(courseCode) { }

        public override string GetSql() => Delete.FromTable(TableNames.Course, where: "CourseCode = @CourseCode");
    }
}
