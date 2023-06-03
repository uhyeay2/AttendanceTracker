using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers
{
    public class IsStudentAttendanceOccurenceExistingRequest : RequiredGuidRequest<bool>
    {
        public IsStudentAttendanceOccurenceExistingRequest() { }

        public IsStudentAttendanceOccurenceExistingRequest(Guid guid) : base(guid) { }
    }

    internal class IsStudentAttendanceOccurenceExistingHandler : DataIsExistingHandler<IsStudentAttendanceOccurenceExistingRequest>
    {
        public IsStudentAttendanceOccurenceExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        protected override IDataRequest<bool> InitializeFetchRequest(IsStudentAttendanceOccurenceExistingRequest request) =>
            new IsStudentAttendanceOccurenceExisting(request.Guid);
    }
}
