using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.RequestHandlers.CourseHandlers
{
    public class IsCourseCodeExistingRequest : RequiredCodeRequest<bool> { }

    internal class IsCourseCodeExistingHandler : DataHandler<IsCourseCodeExistingRequest, bool>
    {
        public IsCourseCodeExistingHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<bool> HandleRequestAsync(IsCourseCodeExistingRequest request)
        {
            return await _dataAccess.FetchAsync(new IsCourseCodeExisting(request.Code));
        }
    }
}
