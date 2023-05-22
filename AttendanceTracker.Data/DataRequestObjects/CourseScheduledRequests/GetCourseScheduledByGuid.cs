namespace AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests
{
    public class GetCourseScheduledByGuid : Guid_DataRequest<CourseScheduled_DTO>
    {
        public GetCourseScheduledByGuid(Guid guid) : base(guid) { }

        public override string GetSql() => Select.FromTable(TableNames.CourseScheduled, where: "Guid = @Guid");
    }
}
