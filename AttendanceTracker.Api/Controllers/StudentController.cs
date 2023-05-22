using AttendanceTracker.Application.Abstraction.Interfaces;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.Api.Controllers
{
    public class StudentController : BaseController
    {
        public StudentController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpPost("InsertStudent")]
        public async Task<Student> InsertStudent(InsertStudentRequest request) => 
            await _orchestrator.GetResponseAsync<InsertStudentRequest, Student>(request);

        [HttpDelete("DeleteStudent")]
        public async Task DeleteStudent(DeleteStudentRequest request) => 
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetStudentByStudentCode")]
        public async Task<Student> GetStudentByStudentCode(GetStudentByStudentCodeRequest request) => 
            await _orchestrator.GetResponseAsync<GetStudentByStudentCodeRequest, Student>(request);

        [HttpGet("GetStudentsByName")]
        public async Task<IEnumerable<Student>> GetStudentsByName(GetStudentsByNameRequest request) => 
            await _orchestrator.GetResponseAsync<GetStudentsByNameRequest, IEnumerable<Student>>(request);

        [HttpPost("UpdateStudent")]
        public async Task UpdateStudent(UpdateStudentRequest request) =>
            await _orchestrator.ExecuteRequestAsync(request);
    }
}
