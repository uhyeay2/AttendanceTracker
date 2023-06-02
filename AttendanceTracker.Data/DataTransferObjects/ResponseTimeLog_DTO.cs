namespace AttendanceTracker.Data.DataTransferObjects
{
    public class ResponseTimeLog_DTO
    {
        public int Id { get; set; }
        public DateTime DateTimeRequestWasReceivedInUTC { get; set; }
        public string RequestPath { get; set; } = null!;
        public long ResponseTimeInMilliseconds { get; set; }

        public ResponseTimeLog AsResponseTimeLog() => new(DateTimeRequestWasReceivedInUTC, RequestPath, ResponseTimeInMilliseconds);
    }
}
