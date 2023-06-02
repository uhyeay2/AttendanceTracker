namespace AttendanceTracker.Data.DataTransferObjects
{
    public class ResponseTimeDetails_DTO
    {
        public string RequestPath { get; set; } = string.Empty;
        public int CountOfTimesCalled { get; set; }
        public double AverageResponseTime { get; set; }
        public long LongestResponseTime { get; set; }
        public long ShortestResponseTime { get; set; }

        public RequestResponseTimeDetails AsRequestResponseTimeDetails() =>
            new(RequestPath, CountOfTimesCalled, AverageResponseTime, LongestResponseTime, ShortestResponseTime);
    }
}
