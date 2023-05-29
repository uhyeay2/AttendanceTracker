using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.RequestHandlers.CourseHandlers
{
    public class IsCourseCodeExistingRequest : RequiredCodeRequest<bool>
    {
        public IsCourseCodeExistingRequest() { }

        public IsCourseCodeExistingRequest(string code) : base(code) { }
    }

    internal class IsCourseCodeExistingHandler : DataIsExistingHandler<IsCourseCodeExistingRequest>
    {
        public IsCourseCodeExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        protected override IDataRequest<bool> InitializeFetchRequest(IsCourseCodeExistingRequest request) =>
            new IsCourseCodeExisting(request.Code);
    }
}
