using AttendanceTracker.Application.Abstraction.Interfaces;
using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.Api.Controllers
{
    public class CourseScheduledController : BaseController
    {
        public CourseScheduledController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertCourseScheduled")]
        public async Task<CourseScheduled> InsertCourseScheduled(InsertCourseScheduledRequest request) =>
       await _orchestrator.GetResponseAsync<InsertCourseScheduledRequest, CourseScheduled>(request);

        [HttpDelete("DeleteCourseScheduled")]
        public async Task Deletetudent(DeleteCourseScheduledRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetCourseScheduledByGuid")]
        public async Task<CourseScheduled> GetCourseScheduledByGuid(GetCourseScheduledByGuidRequest request) =>
            await _orchestrator.GetResponseAsync<GetCourseScheduledByGuidRequest, CourseScheduled>(request);
    }
}
