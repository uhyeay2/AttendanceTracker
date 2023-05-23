namespace AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests
{
    public class IsCourseScheduledGuidExisting : Guid_DataRequest<bool>
    {
        public IsCourseScheduledGuidExisting(Guid guid) : base(guid) { }

        public override string GetSql() => Select.Exists(TableNames.CourseScheduled, where: "Guid = @Guid");
    }
}
