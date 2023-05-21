namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class IsCourseCodeExisting : Code_DataRequest<bool>
    {
        public IsCourseCodeExisting(string courseCode) : base(courseCode) { }

        public override string GetSql() => Select.Exists(TableNames.Course, where: "CourseCode = @Code");
    }
}
