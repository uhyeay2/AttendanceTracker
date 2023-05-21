namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class GetCourseByCourseCode : Code_DataRequest<Course_DTO>
    {
        public GetCourseByCourseCode(string code) : base(code) { }

        public override string GetSql() => Select.FromTable(TableNames.Course, where: "CourseCode = @Code");
    }
}
