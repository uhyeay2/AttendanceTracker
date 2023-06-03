namespace AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests
{
    public class DeleteStudentAttendanceOccurence : Guid_DataRequest
    {
        public DeleteStudentAttendanceOccurence(Guid guid) : base(guid) { }

        public override string GetSql() => $@"            
            DECLARE @StudentAttendanceOccurenceId INT = ( 
                {Select.JoinFromTable(TableNames.StudentAttendanceOccurence, 
                    $@"JOIN {TableNames.AttendanceOccurence} ON {TableNames.AttendanceOccurence}.Id = {TableNames.StudentAttendanceOccurence}.AttendanceOccurenceId",
                    columns: $"{TableNames.StudentAttendanceOccurence}.Id", where: "Guid = @Guid" )} )

            {Delete.FromTable(TableNames.StudentAttendanceOccurence, where: "Id = @StudentAttendanceOccurenceId")} 

            {Delete.FromTable(TableNames.AttendanceOccurence, where: "Guid = @Guid")} 
        ";
    }
}
