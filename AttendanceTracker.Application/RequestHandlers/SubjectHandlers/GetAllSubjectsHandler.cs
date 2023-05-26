using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.RequestHandlers.SubjectHandlers
{
    public class GetAllSubjectsRequest : IRequest<IEnumerable<Subject>> { }

    internal class GetAllSubjectsHandler : DataHandler<GetAllSubjectsRequest, IEnumerable<Subject>>
    {
        public GetAllSubjectsHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<IEnumerable<Subject>> HandleRequestAsync(GetAllSubjectsRequest request)
        {
            var dto = await _dataAccess.FetchListAsync(new GetAllSubjects());

            return dto.Any() ? dto.Select(_ => _.AsSubject()) : Enumerable.Empty<Subject>();
        }
    }
}
