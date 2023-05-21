using AttendanceTracker.Application.Abstraction.Interfaces;
using AttendanceTracker.Application.RequestHandlers.SubjectHandlers;
using AttendanceTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.Api.Controllers
{
    public class SubjectController : BaseController
    {
        public SubjectController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertSubject")]
        public async Task<Subject> InsertSubject(InsertSubjectRequest request) =>
          await _orchestrator.GetResponseAsync<InsertSubjectRequest, Subject>(request);

        [HttpPost("DeleteSubject")]
        public async Task Deletetudent(DeleteSubjectRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetSubjectBySubjectCode")]
        public async Task<Subject> GetSubjectBySubjectCode(GetSubjectByCodeRequest request) =>
            await _orchestrator.GetResponseAsync<GetSubjectByCodeRequest, Subject>(request);

        [HttpGet("IsSubjectCodeExisting")]
        public async Task<bool> IsSubjectCodeExisting(IsSubjectCodeExistingRequest request) =>
            await _orchestrator.GetResponseAsync<IsSubjectCodeExistingRequest, bool>(request);
    }
}
