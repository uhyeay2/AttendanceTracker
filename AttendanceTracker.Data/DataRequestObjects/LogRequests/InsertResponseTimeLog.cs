namespace AttendanceTracker.Data.DataRequestObjects.LogRequests
{
    public class InsertResponseTimeLog : IDataRequest
    {
        public InsertResponseTimeLog(DateTime dateTimeRequestWasReceivedInUTC, string requestPath, long responseTimeInMilliseconds)
        {
            DateTimeRequestWasReceivedInUTC = dateTimeRequestWasReceivedInUTC;
            RequestPath = requestPath;
            ResponseTimeInMilliseconds = responseTimeInMilliseconds;
        }

        public DateTime DateTimeRequestWasReceivedInUTC { get; set; }
        public string RequestPath { get; set; }
        public long ResponseTimeInMilliseconds { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => Insert.IntoTable(TableNames.ResponseTimeLog,
            "DateTimeRequestWasReceivedInUTC", "RequestPath", "ResponseTimeInMilliseconds");
    }
}
