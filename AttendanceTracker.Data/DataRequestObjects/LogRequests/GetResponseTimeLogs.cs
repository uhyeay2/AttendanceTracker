namespace AttendanceTracker.Data.DataRequestObjects.LogRequests
{
    public class GetResponseTimeLogs : Parameterless_DataRequest<ResponseTimeLog>
    {
        public override string GetSql() => Select.FromTable(TableNames.ResponseTimeLog);
    }
}
