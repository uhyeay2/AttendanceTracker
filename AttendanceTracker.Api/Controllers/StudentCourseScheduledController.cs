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
        public async Task DeleteStudentCourseScheduled(DeleteStudentCourseScheduledRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetAllStudentCourseScheduledByStudentCode")]
        public async Task<StudentCoursesScheduled> GetAllCoursesScheduledByStudentCode(string studentCode) =>
            await _orchestrator.GetResponseAsync<GetAllStudentCourseScheduledByStudentCodeRequest, StudentCoursesScheduled>(new (studentCode));

        [HttpGet("GetStudentCourseScheduled")]
        public async Task<StudentCourseScheduled> GetStudentCourseScheduled(GetStudentCourseScheduledRequest request) =>
            await _orchestrator.GetResponseAsync<GetStudentCourseScheduledRequest, StudentCourseScheduled>(request);
    }
}
