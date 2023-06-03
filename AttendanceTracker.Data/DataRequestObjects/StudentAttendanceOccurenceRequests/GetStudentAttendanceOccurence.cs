namespace AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests
{
    public class GetStudentAttendanceOccurence : Guid_DataRequest<StudentAttendanceOccurence_DTO>
    {
        public GetStudentAttendanceOccurence(Guid guid) : base(guid) { }

        public override string GetSql() =>
            Select.JoinFromTable(TableNames.AttendanceOccurence,
                joins:$"JOIN {TableNames.StudentAttendanceOccurence} ON {TableNames.StudentAttendanceOccurence}.AttendanceOccurenceId = {TableNames.AttendanceOccurence}.Id",
                columns: @$"{TableNames.StudentAttendanceOccurence}.*, {TableNames.AttendanceOccurence}.*", 
                where: "Guid = @Guid");
    }
}
