using AttendanceTracker.Application.RequestHandlers.ExampleHandlers;
using Microsoft.AspNetCore.Mvc;
using AttendanceTracker.Application.Abstraction.Interfaces;

namespace AttendanceTracker.Api.Controllers
{
    public class ExampleController : Controller
    {
        private readonly IOrchestrator _orchestrator;

        public ExampleController(IOrchestrator orchestrator) => _orchestrator = orchestrator;

        [HttpGet("GetSumDefined")]
        public async Task<int> GetSumDefined(GetSumRequest request) => await _orchestrator.GetResponseAsync<GetSumRequest, int>(request);
    }
}
