using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.RequestHandlers.SubjectHandlers
{
    public class IsSubjectCodeExistingRequest : RequiredCodeRequest<bool>
    {
        public IsSubjectCodeExistingRequest() { }

        public IsSubjectCodeExistingRequest(string code) : base(code) { }
    }

    internal class IsSubjectCodeExistingHandler : DataIsExistingHandler<IsSubjectCodeExistingRequest>
    {
        public IsSubjectCodeExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        protected override IDataRequest<bool> InitializeFetchRequest(IsSubjectCodeExistingRequest request) =>
            new IsSubjectCodeExisting(request.Code);
    }
}
