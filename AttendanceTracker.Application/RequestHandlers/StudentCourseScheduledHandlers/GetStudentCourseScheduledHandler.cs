using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
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

            var courseScheduledDTO = await _dataAccess.FetchAsync(new GetStudentCourseScheduled(request.StudentCode, request.CourseScheduledGuid));

            if (courseScheduledDTO == null)
            {
                throw new DoesNotExistException(typeof(StudentCourseScheduled), (request.StudentCode, nameof(request.StudentCode)),
                                                                                (request.CourseScheduledGuid, nameof(request.CourseScheduledGuid)));
            }

            var course = await _orchestrator.GetResponseAsync<GetCourseScheduledByGuidRequest, CourseScheduled>(new(courseScheduledDTO.Guid));

            return new StudentCourseScheduled(student, course);
        }
    }
}
