namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class GetCourseByCourseCode : CourseCode_DataRequest<Course_DTO>
    {
        public GetCourseByCourseCode(string courseCode) : base(courseCode) { }

        public override string GetSql() => Select.FromTable(TableNames.Course, where: "CourseCode = @CourseCode");
    }
}
