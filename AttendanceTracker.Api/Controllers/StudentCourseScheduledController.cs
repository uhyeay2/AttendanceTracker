using AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers;

namespace AttendanceTracker.Api.Controllers
{
    public class StudentCourseScheduledController : BaseController
    {
        public StudentCourseScheduledController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertStudentCourseScheduled")]
        public async Task<StudentCourseScheduled> InsertCourseScheduled(InsertStudentCourseScheduledRequest request) =>
            await _orchestrator.GetResponseAsync<InsertStudentCourseScheduledRequest, StudentCourseScheduled>(request);

        [HttpDelete("DeleteStudentCourseScheduled")]
        public async Task Deletetudent(DeleteStudentCourseScheduledRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetCourseStudentScheduledByGuid")]
        public async Task<StudentCourseScheduled> GetCourseScheduledByGuid(GetStudentCourseScheduledRequest request) =>
            await _orchestrator.GetResponseAsync<GetStudentCourseScheduledRequest, StudentCourseScheduled>(request);
    }
}
