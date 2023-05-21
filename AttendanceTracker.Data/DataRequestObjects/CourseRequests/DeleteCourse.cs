namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class DeleteCourse : Code_DataRequest
    {
        public DeleteCourse(string courseCode) : base(courseCode) { }

        public override string GetSql() => Delete.FromTable(TableNames.Course, where: "CourseCode = @Code");
    }
}
