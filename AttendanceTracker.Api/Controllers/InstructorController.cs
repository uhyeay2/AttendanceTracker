using AttendanceTracker.Application.Abstraction.Interfaces;
using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;
using AttendanceTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.Api.Controllers
{
    public class InstructorController : BaseController
    {
        public InstructorController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertInstructor")]
        public async Task<Instructor> InsertInstructor(InsertInstructorRequest request) =>
            await _orchestrator.GetResponseAsync<InsertInstructorRequest, Instructor>(request);

        [HttpDelete("DeleteInstructor")]
        public async Task DeleteInstructor(DeleteInstructorRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetInstructorByInstructorCode")]
        public async Task<Instructor> GetInstructorByInstructorCode(GetInstructorByInstructorCodeRequest request) =>
            await _orchestrator.GetResponseAsync<GetInstructorByInstructorCodeRequest, Instructor>(request);

        [HttpPost("UpdateInstructor")]
        public async Task UpdateInstructor(UpdateInstructorRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);
    }
}
