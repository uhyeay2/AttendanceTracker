using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class IsStudentCodeExistingRequest : RequiredCodeRequest<bool>
    {
        public IsStudentCodeExistingRequest() { }

        public IsStudentCodeExistingRequest(string code) : base(code) { }
    }

    internal class IsStudentCodeExistingHandler : DataHandler<IsStudentCodeExistingRequest, bool>
    {
        public IsStudentCodeExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<bool> HandleRequestAsync(IsStudentCodeExistingRequest request) =>
            await _dataAccess.FetchAsync(new IsStudentCodeExisting(request.Code));
    }
}
