using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers
{
    public class GetStudentCourseScheduledRequest : RequiredStudentCodeAndCourseScheduledGuidRequest<StudentCourseScheduled>
    {
        public GetStudentCourseScheduledRequest() { }

        public GetStudentCourseScheduledRequest(string studentCode, Guid courseScheduledGuid) : base(studentCode, courseScheduledGuid) { }
    }

    internal class GetStudentCourseScheduledHandler : DataOrchestratorHandler<GetStudentCourseScheduledRequest, StudentCourseScheduled>
    {
        public GetStudentCourseScheduledHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<StudentCourseScheduled> HandleRequestAsync(GetStudentCourseScheduledRequest request)
        {
            var student = await _orchestrator.GetResponseAsync<GetStudentByStudentCodeRequest, Student>(new(request.StudentCode));

            var dto = await _dataAccess.FetchAsync(new GetStudentCourseScheduled(request.StudentCode, request.CourseScheduledGuid));

            if (dto == null)
            {
                throw new DoesNotExistException(typeof(StudentCourseScheduled), (request.StudentCode, nameof(request.StudentCode)), 
                                                                                (request.CourseScheduledGuid, nameof(request.CourseScheduledGuid)));
            }

            var fetchInstructorTask = _dataAccess.FetchAsync(new GetInstructorById(dto.InstructorId));
            var fetchCourseTask = _dataAccess.FetchAsync(new GetCourseById(dto.CourseId));

            await Task.WhenAll(fetchInstructorTask, fetchCourseTask);

            var course = await fetchCourseTask;
            var instructor = await fetchInstructorTask;

            var courseScheduled = dto.AsCourseScheduled(course, instructor);

            return new StudentCourseScheduled(student, courseScheduled);
        }
    }
}
