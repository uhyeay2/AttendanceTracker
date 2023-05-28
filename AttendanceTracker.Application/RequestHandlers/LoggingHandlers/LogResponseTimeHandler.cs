using AttendanceTracker.Data.DataRequestObjects.LogRequests;

namespace AttendanceTracker.Application.RequestHandlers.LoggingHandlers
{
    public class LogResponseTimeRequest : IRequest
    {
        public LogResponseTimeRequest(DateTime dateTimeRequestWasReceivedInUTC, string? requestUrl, long responseTimeInMilliseconds)
        {
            DateTimeRequestWasReceivedInUTC = dateTimeRequestWasReceivedInUTC;
            RequestUrl = requestUrl ?? "No Request.Path Found";
            ResponseTimeInMilliseconds = responseTimeInMilliseconds;
        }

        public DateTime DateTimeRequestWasReceivedInUTC { get; set; }
        public string RequestUrl { get; set; }
        public long ResponseTimeInMilliseconds { get; set; }
    }

    internal class LogResponseTimeHandler : DataHandler<LogResponseTimeRequest>
    {
        public LogResponseTimeHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(LogResponseTimeRequest request) =>
            await _dataAccess.ExecuteAsync(new InsertResponseTimeLog
                (request.DateTimeRequestWasReceivedInUTC, request.RequestUrl, request.ResponseTimeInMilliseconds));
    }
}
