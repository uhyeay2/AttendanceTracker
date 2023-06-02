using AttendanceTracker.Data.DataRequestObjects.LogRequests;

namespace AttendanceTracker.Application.RequestHandlers.LoggingHandlers
{
    public class InsertResponseTimeLogRequest : IRequest
    {
        public InsertResponseTimeLogRequest(DateTime dateTimeRequestWasReceivedInUTC, string? requestUrl, long responseTimeInMilliseconds)
        {
            DateTimeRequestWasReceivedInUTC = dateTimeRequestWasReceivedInUTC;
            RequestUrl = requestUrl ?? "No Request.Path Found";
            ResponseTimeInMilliseconds = responseTimeInMilliseconds;
        }

        public DateTime DateTimeRequestWasReceivedInUTC { get; set; }
        public string RequestUrl { get; set; }
        public long ResponseTimeInMilliseconds { get; set; }
    }

    internal class InsertResponseTimeLogHandler : DataHandler<InsertResponseTimeLogRequest>
    {
        public InsertResponseTimeLogHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(InsertResponseTimeLogRequest request) =>
            await _dataAccess.ExecuteAsync(new InsertResponseTimeLog
                (request.DateTimeRequestWasReceivedInUTC, request.RequestUrl, request.ResponseTimeInMilliseconds));
    }
}
