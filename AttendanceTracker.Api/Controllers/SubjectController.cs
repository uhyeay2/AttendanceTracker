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
        public async Task DeleteSubject(string subjectCode) =>
            await _orchestrator.ExecuteRequestAsync(new DeleteSubjectRequest(subjectCode));

        [HttpGet("GetAllSubjects")]
        public async Task<IEnumerable<Subject>> GetAllSubjects() =>
          await _orchestrator.GetResponseAsync<GetAllSubjectsRequest, IEnumerable<Subject>>(new());

        [HttpGet("GetSubjectBySubjectCode")]
        public async Task<Subject> GetSubjectBySubjectCode(string subjectCode) =>
            await _orchestrator.GetResponseAsync<GetSubjectByCodeRequest, Subject>(new GetSubjectByCodeRequest(subjectCode));

        [HttpGet("IsSubjectCodeExisting")]
        public async Task<bool> IsSubjectCodeExisting(string subjectCode) =>
            await _orchestrator.GetResponseAsync<IsSubjectCodeExistingRequest, bool>(new IsSubjectCodeExistingRequest(subjectCode));
    }
}
