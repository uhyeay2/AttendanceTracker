using AttendanceTracker.Data.DataRequestObjects.LogRequests;

namespace AttendanceTracker.Application.RequestHandlers.LoggingHandlers
{
    public class GetResponseTimeDetailsRequest : IRequest<OverallResponseTimeDetails>
    {
        public GetResponseTimeDetailsRequest() { }

        public GetResponseTimeDetailsRequest(DateTime? startDate, DateTime? endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    internal class GetResponseTimeDetailsHandler : DataHandler<GetResponseTimeDetailsRequest, OverallResponseTimeDetails>
    {
        public GetResponseTimeDetailsHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<OverallResponseTimeDetails> HandleRequestAsync(GetResponseTimeDetailsRequest request)
        {
            var dto = await _dataAccess.FetchListAsync(new GetResponseTimeDetails(request.StartDate, request.EndDate));

            if (!dto.Any())
            {
                throw new DoesNotExistException(typeof(ResponseTimeLog), (request.StartDate, nameof(request.StartDate)),
                                                                         (request.EndDate, nameof(request.EndDate)));
            }

            var requests = dto.Select(_ => _.AsRequestResponseTimeDetails());

            return new OverallResponseTimeDetails(requests);
        }
    }
}
