using AttendanceTracker.Application.RequestHandlers.CourseHandlers;

namespace AttendanceTracker.Api.Controllers
{
    public class CourseController : BaseController
    {
        public CourseController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertCourse")]
        public async Task<Course> InsertCourse(InsertCourseRequest request) =>
            await _orchestrator.GetResponseAsync<InsertCourseRequest, Course>(request);

        [HttpDelete("DeleteCourse")]
        public async Task Deletetudent(DeleteCourseRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetCourseByCourseCode")]
        public async Task<Course> GetCourseByCourseCode(GetCourseByCodeRequest request) =>
            await _orchestrator.GetResponseAsync<GetCourseByCodeRequest, Course>(request);

        [HttpPost("UpdateCourse")]
        public async Task UpdateCourse(UpdateCourseRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);
    }
}
