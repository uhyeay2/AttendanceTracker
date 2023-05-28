namespace AttendanceTracker.Domain.Models
{
    public class ResponseTimeLog
    {
        public ResponseTimeLog(DateTime dateTimeRequestWasReceivedInUTC, string requestUrl, long responseTimeInMilliseconds)
        {
            DateTimeRequestWasReceivedInUTC = dateTimeRequestWasReceivedInUTC;
            RequestUrl = requestUrl;
            ResponseTimeInMilliseconds = responseTimeInMilliseconds;
        }

        public ResponseTimeLog() { }

        public DateTime DateTimeRequestWasReceivedInUTC { get; set; }
        public string RequestUrl { get; set; } = null!;
        public long ResponseTimeInMilliseconds { get; set; }
    }
}
