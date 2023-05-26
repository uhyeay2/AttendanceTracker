using AttendanceTracker.Application.RequestHandlers.SubjectHandlers;

namespace AttendanceTracker.Api.Controllers
{
    public class SubjectController : BaseController
    {
        public SubjectController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertSubject")]
        public async Task<Subject> InsertSubject(InsertSubjectRequest request) =>
          await _orchestrator.GetResponseAsync<InsertSubjectRequest, Subject>(request);

        [HttpDelete("DeleteSubject")]
        public async Task Deletetudent(DeleteSubjectRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetAllSubjects")]
        public async Task<IEnumerable<Subject>> GetAllSubjects() =>
          await _orchestrator.GetResponseAsync<GetAllSubjectsRequest, IEnumerable<Subject>>(new());

        [HttpGet("GetSubjectBySubjectCode")]
        public async Task<Subject> GetSubjectBySubjectCode(GetSubjectByCodeRequest request) =>
            await _orchestrator.GetResponseAsync<GetSubjectByCodeRequest, Subject>(request);

        [HttpGet("IsSubjectCodeExisting")]
        public async Task<bool> IsSubjectCodeExisting(IsSubjectCodeExistingRequest request) =>
            await _orchestrator.GetResponseAsync<IsSubjectCodeExistingRequest, bool>(request);
    }
}
