using AttendanceTracker.Application.Abstraction.Interfaces;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.Api.Controllers
{
    public class StudentController : Controller
    {
        private readonly IOrchestrator _orchestrator;

        public StudentController(IOrchestrator orchestrator) => _orchestrator = orchestrator;

        [HttpPost("InsertStudent")]
        public async Task<Student> InsertStudent(InsertStudentRequest request) => 
            await _orchestrator.GetResponseAsync<InsertStudentRequest, Student>(request);

        [HttpPost("DeleteStudent")]
        public async Task Deletetudent(DeleteStudentRequest request) => 
            await _orchestrator.ExecuteRequestAsync(request);

        [HttpGet("GetStudentByStudentCode")]
        public async Task<Student> GetStudentByStudentCode(GetStudentByStudentCodeRequest request) => 
            await _orchestrator.GetResponseAsync<GetStudentByStudentCodeRequest, Student>(request);

        [HttpGet("GetStudentsByName")]
        public async Task<IEnumerable<Student>> GetStudentsByName(GetStudentsByNameRequest request) => 
            await _orchestrator.GetResponseAsync<GetStudentsByNameRequest, IEnumerable<Student>>(request);
    }
}
