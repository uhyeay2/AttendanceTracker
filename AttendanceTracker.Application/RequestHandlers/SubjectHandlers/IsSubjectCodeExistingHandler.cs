using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.RequestHandlers.SubjectHandlers
{
    public class IsSubjectCodeExistingRequest : RequiredCodeRequest<bool>
    {
        public IsSubjectCodeExistingRequest() { }

        public IsSubjectCodeExistingRequest(string code) : base(code) { }
    }

    internal class IsSubjectCodeExistingHandler : DataHandler<IsSubjectCodeExistingRequest, bool>
    {
        public IsSubjectCodeExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<bool> HandleRequestAsync(IsSubjectCodeExistingRequest request)
        {
            return await _dataAccess.FetchAsync(new IsSubjectCodeExisting(request.Code));
        }
    }
}
