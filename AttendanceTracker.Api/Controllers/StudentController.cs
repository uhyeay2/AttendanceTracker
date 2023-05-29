using AttendanceTracker.Application.RequestHandlers.StudentHandlers;

namespace AttendanceTracker.Api.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertStudent")]
        public async Task<Student> InsertStudent(InsertStudentRequest request) => 
            await _orchestrator.GetResponseAsync<InsertStudentRequest, Student>(request);

        [HttpDelete("DeleteStudent")]
        public async Task DeleteStudent(string studentCode) => 
            await _orchestrator.ExecuteRequestAsync(new DeleteStudentRequest(studentCode));

        [HttpGet("GetAllStudentsPaginated")]
        public async Task<IEnumerable<Student>> GetAllStudentsPaginated(int pageNumber, int recordsPerPage) =>
            await _orchestrator.GetResponseAsync<GetAllStudentsPaginatedRequest, IEnumerable<Student>>(new (pageNumber, recordsPerPage));

        [HttpGet("GetStudentByStudentCode")]
        public async Task<Student> GetStudentByStudentCode(string studentCode) => 
            await _orchestrator.GetResponseAsync<GetStudentByStudentCodeRequest, Student>(new(studentCode));

        [HttpGet("GetStudentsByName")]
        public async Task<IEnumerable<Student>> GetStudentsByName(GetStudentsByNameRequest request) => 
            await _orchestrator.GetResponseAsync<GetStudentsByNameRequest, IEnumerable<Student>>(request);

        [HttpGet("IsStudentCodeExisting")]
        public async Task<bool> IsStudentCodeExisting(string studentCode) =>
            await _orchestrator.GetResponseAsync<IsStudentCodeExistingRequest, bool>(new(studentCode));

        [HttpPost("UpdateStudent")]
        public async Task UpdateStudent(UpdateStudentRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);
    }
}
