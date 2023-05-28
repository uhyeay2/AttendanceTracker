using AttendanceTracker.Data.DataRequestObjects.LogRequests;

namespace AttendanceTracker.Application.RequestHandlers.LoggingHandlers
{
    public class GetAllLoggedResponseTimesRequest : IRequest<IEnumerable<ResponseTimeLog>> { }

    internal class GetAllLoggedResponseTimesHandler : DataHandler<GetAllLoggedResponseTimesRequest, IEnumerable<ResponseTimeLog>>
    {
        public GetAllLoggedResponseTimesHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<IEnumerable<ResponseTimeLog>> HandleRequestAsync(GetAllLoggedResponseTimesRequest request)
        {
            var logs =  await _dataAccess.FetchListAsync(new GetResponseTimeLogs());
        
            return logs.Any() ? logs.OrderByDescending(_ => _.DateTimeRequestWasReceivedInUTC) : Enumerable.Empty<ResponseTimeLog>();
        }
    }
}
