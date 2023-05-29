using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.RequestHandlers.InstructorHandlers
{
    public class IsInstructorCodeExistingRequest : RequiredCodeRequest<bool>
    {
        public IsInstructorCodeExistingRequest() { }

        public IsInstructorCodeExistingRequest(string code) : base(code) { }
    }

    internal class IsInstructorCodeExistingHandler : DataIsExistingHandler<IsInstructorCodeExistingRequest>
    {
        public IsInstructorCodeExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        protected override IDataRequest<bool> InitializeFetchRequest(IsInstructorCodeExistingRequest request) => 
            new IsInstructorCodeExisting(request.Code);
    }
}
