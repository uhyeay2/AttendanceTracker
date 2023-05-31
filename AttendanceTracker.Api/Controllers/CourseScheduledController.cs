using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;

namespace AttendanceTracker.Api.Controllers
{
    public class CourseScheduledController : BaseController
    {
        public CourseScheduledController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertCourseScheduled")]
        public async Task<CourseScheduled> InsertCourseScheduled(InsertCourseScheduledRequest request) =>
       await _orchestrator.GetResponseAsync<InsertCourseScheduledRequest, CourseScheduled>(request);

        [HttpDelete("DeleteCourseScheduled")]
        public async Task DeleteCourseScheduled(Guid guid) =>
            await _orchestrator.ExecuteRequestAsync(new DeleteCourseScheduledRequest(guid));

        [HttpGet("GetCourseScheduledByGuid")]
        public async Task<CourseScheduled> GetCourseScheduledByGuid(Guid guid) =>
            await _orchestrator.GetResponseAsync<GetCourseScheduledByGuidRequest, CourseScheduled>(new(guid));
    }
}
