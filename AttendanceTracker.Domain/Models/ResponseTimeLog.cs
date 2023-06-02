namespace AttendanceTracker.Domain.Models
{
    public class ResponseTimeLog
    {
        public ResponseTimeLog(DateTime dateTimeRequestWasReceivedInUTC, string requestPath, long responseTimeInMilliseconds)
        {
            DateTimeRequestWasReceivedInUTC = dateTimeRequestWasReceivedInUTC;
            RequestPath = requestPath;
            ResponseTimeInMilliseconds = responseTimeInMilliseconds;
        }

        public ResponseTimeLog() { }

        public DateTime DateTimeRequestWasReceivedInUTC { get; set; }
        public string RequestPath { get; set; } = null!;
        public long ResponseTimeInMilliseconds { get; set; }
    }
}
