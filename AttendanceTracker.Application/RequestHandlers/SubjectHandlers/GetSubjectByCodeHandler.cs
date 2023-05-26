using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.RequestHandlers.SubjectHandlers
{
    public class GetSubjectByCodeRequest : RequiredCodeRequest<Subject>
    {
        public GetSubjectByCodeRequest() { }

        public GetSubjectByCodeRequest(string code) : base(code) { }
    }

    internal class GetSubjectByCodeHandler : DataHandler<GetSubjectByCodeRequest, Subject>
    {
        public GetSubjectByCodeHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public async override Task<Subject> HandleRequestAsync(GetSubjectByCodeRequest request)
        {
            var dto = await _dataAccess.FetchAsync(new GetSubjectByCode(request.Code));

            return dto != null ? dto.AsSubject() : throw new DoesNotExistException(typeof(Subject), request.Code, nameof(request.Code));
        }
    }
}
