namespace AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests
{
    public class DeleteCourseScheduled : Guid_DataRequest
    {
        public DeleteCourseScheduled(Guid guid) : base(guid) { }

        public override string GetSql() => Delete.FromTable(TableNames.CourseScheduled, where: "Guid = @Guid");
    }
}
