namespace AttendanceTracker.Data.DataRequestObjects.LogRequests
{
    public class InsertResponseTimeLog : IDataRequest
    {
        public InsertResponseTimeLog(DateTime dateTimeRequestWasReceivedInUTC, string requestUrl, long responseTimeInMilliseconds)
        {
            DateTimeRequestWasReceivedInUTC = dateTimeRequestWasReceivedInUTC;
            RequestUrl = requestUrl;
            ResponseTimeInMilliseconds = responseTimeInMilliseconds;
        }

        public DateTime DateTimeRequestWasReceivedInUTC { get; set; }
        public string RequestUrl { get; set; }
        public long ResponseTimeInMilliseconds { get; set; }

        public object? GetParameters() => this;

        public string GetSql() => Insert.IntoTable(TableNames.ResponseTimeLog,
            "DateTimeRequestWasReceivedInUTC", "RequestUrl", "ResponseTimeInMilliseconds");
    }
}
