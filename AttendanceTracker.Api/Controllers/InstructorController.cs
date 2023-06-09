﻿using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;

namespace AttendanceTracker.Api.Controllers
{
    public class InstructorController : BaseController
    {
        public InstructorController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertInstructor")]
        public async Task<Instructor> InsertInstructor(InsertInstructorRequest request) =>
            await _orchestrator.GetResponseAsync<InsertInstructorRequest, Instructor>(request);

        [HttpDelete("DeleteInstructor")]
        public async Task DeleteInstructor(string instructorCode) =>
            await _orchestrator.ExecuteRequestAsync(new DeleteInstructorRequest(instructorCode));

        [HttpGet("GetInstructorByInstructorCode")]
        public async Task<Instructor> GetInstructorByInstructorCode(string instructorCode) =>
            await _orchestrator.GetResponseAsync<GetInstructorByInstructorCodeRequest, Instructor>(new (instructorCode));

        [HttpGet("IsInstructorCodeExisting")]
        public async Task<bool> IsInstructorCodeExisting(string instructorCode) =>
            await _orchestrator.GetResponseAsync<IsInstructorCodeExistingRequest, bool>(new(instructorCode));

        [HttpPost("UpdateInstructor")]
        public async Task UpdateInstructor(UpdateInstructorRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);
    }
}
