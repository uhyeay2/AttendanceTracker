namespace AttendanceTracker.Data.DataRequestObjects.LogRequests
{
    public class GetAverageResponseTimeForAllRequests : IDataRequest<AverageResponseTimeForAllRequests_DTO>
    {
        public GetAverageResponseTimeForAllRequests() { }

        public GetAverageResponseTimeForAllRequests(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime? StartDate { get; set; } = null;

        public DateTime? EndDate { get; set; } = null;

        public object? GetParameters() => new { StartDate, EndDate };

        public string GetSql() => 
            Select.FromTable(TableNames.ResponseTimeLog,
            columns:
            @" 
                RequestPath, 
                AVG(ResponseTimeInMilliseconds) AverageResponseTime,
                MAX(ResponseTimeInMilliseconds) LongestResponseTime, 
                MIN(ResponseTimeInMilliseconds) ShortestResponseTime, 
                Count(Id) CountOfTimesCalled
            ",
            where: @" ( @StartDate IS NULL OR DateTimeRequestWasReceivedInUTC >= @StartDate ) 
                  AND ( @EndDate IS NULL OR DateTimeRequestWasReceivedInUTC <= @EndDate )  "
            ) 
            + " GROUP BY RequestPath";
    }
}
