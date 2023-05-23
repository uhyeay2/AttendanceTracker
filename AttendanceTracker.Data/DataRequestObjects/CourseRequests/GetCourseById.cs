namespace AttendanceTracker.Data.DataRequestObjects.CourseRequests
{
    public class GetCourseById : Id_DataRequest<Course_DTO>
    {
        public GetCourseById(int id) : base(id) { }

        public override string GetSql() => Select.FromTable(TableNames.Course, where: "Id = @Id");
    }
}
