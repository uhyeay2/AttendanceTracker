using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers
{
    public class GetCourseScheduledByGuidRequest : RequiredGuidRequest<CourseScheduled>
    {
        public GetCourseScheduledByGuidRequest() { }

        public GetCourseScheduledByGuidRequest(Guid guid) : base(guid) { }
    }

    internal class GetCourseScheduledByGuidHandler : DataHandler<GetCourseScheduledByGuidRequest, CourseScheduled>
    {
        public GetCourseScheduledByGuidHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<CourseScheduled> HandleRequestAsync(GetCourseScheduledByGuidRequest request)
        {
            var dto = await _dataAccess.FetchAsync(new GetCourseScheduledByGuid(request.Guid));

            if(dto == null)
            {
                throw new DoesNotExistException(typeof(CourseScheduled), request.Guid, nameof(request.Guid));
            }

            var fetchInstructorTask = _dataAccess.FetchAsync(new GetInstructorById(dto.InstructorId));
            var fetchCourseTask = _dataAccess.FetchAsync(new GetCourseById(dto.CourseId));

            await Task.WhenAll(fetchInstructorTask, fetchCourseTask);

            var course = await fetchCourseTask;
            var instructor = await fetchInstructorTask;

            return dto.AsCourseScheduled(course, instructor);
        }
    }
}
