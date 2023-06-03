namespace AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests
{
    public class IsStudentAttendanceOccurenceExisting : Guid_DataRequest<bool>
    {
        public IsStudentAttendanceOccurenceExisting(Guid guid) : base(guid) { }

        public override string GetSql() => Select.Exists(TableNames.AttendanceOccurence, where: "Guid = @Guid");
    }
}
