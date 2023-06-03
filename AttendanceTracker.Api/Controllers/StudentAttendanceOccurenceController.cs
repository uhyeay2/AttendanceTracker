using AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers;

namespace AttendanceTracker.Api.Controllers
{
    public class StudentAttendanceOccurenceController : BaseController
    {
        public StudentAttendanceOccurenceController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertStudentAttendanceOccurence")]
        public async Task<StudentAttendanceOccurence> InsertStudentAttendanceOccurence(InsertStudentAttendanceOccurenceRequest request) =>
         await _orchestrator.GetResponseAsync<InsertStudentAttendanceOccurenceRequest, StudentAttendanceOccurence>(request);

        [HttpDelete("DeleteStudentAttendanceOccurence")]
        public async Task DeleteStudentAttendanceOccurence(Guid guid) =>
            await _orchestrator.ExecuteRequestAsync(new DeleteStudentAttendanceOccurenceRequest(guid));

        [HttpGet("GetStudentAttendanceOccurence")]
        public async Task<StudentAttendanceOccurence> GetStudentAttendanceOccurence(Guid guid) =>
            await _orchestrator.GetResponseAsync<GetStudentAttendanceOccurenceRequest, StudentAttendanceOccurence>(new(guid));
    }
}
