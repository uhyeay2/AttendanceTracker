using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.RequestHandlers.CourseHandlers
{
    public class GetCourseByCodeRequest : RequiredCodeRequest<Course>
    {
        public GetCourseByCodeRequest() { }

        public GetCourseByCodeRequest(string code) : base(code) { }
    }

    internal class GetCourseByCodeHandler : DataHandler<GetCourseByCodeRequest, Course>
    {
        public GetCourseByCodeHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public async override Task<Course> HandleRequestAsync(GetCourseByCodeRequest request)
        {
            var dto = await _dataAccess.FetchAsync(new GetCourseByCourseCode(request.Code));

            return dto != null ? dto.AsCourse() : throw new DoesNotExistException(typeof(Course), request.Code, nameof(request.Code));
        }
    }
}
