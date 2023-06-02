namespace AttendanceTracker.Data.DataRequestObjects.LogRequests
{
    public class GetResponseTimeLogs : Parameterless_DataRequest<ResponseTimeLog_DTO>
    {
        public override string GetSql() => Select.FromTable(TableNames.ResponseTimeLog);
    }
}
