using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class IsStudentCodeExistingRequest : RequiredCodeRequest<bool>
    {
        public IsStudentCodeExistingRequest() { }

        public IsStudentCodeExistingRequest(string code) : base(code) { }
    }

    internal class IsStudentCodeExistingHandler : DataIsExistingHandler<IsStudentCodeExistingRequest>
    {
        public IsStudentCodeExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        protected override IDataRequest<bool> InitializeFetchRequest(IsStudentCodeExistingRequest request) =>
            new IsStudentCodeExisting(request.Code);
    }
}
