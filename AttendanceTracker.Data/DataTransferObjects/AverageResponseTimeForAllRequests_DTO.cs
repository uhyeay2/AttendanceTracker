namespace AttendanceTracker.Data.DataTransferObjects
{
    public class AverageResponseTimeForAllRequests_DTO
    {
        public string RequestPath { get; set; } = string.Empty;
        public long AverageResponseTime { get; set; }
        public long LongestResponseTime { get; set; }
        public long ShortestResponseTime { get; set; }
        public int CountOfTimesCalled { get; set; }
    }
}
