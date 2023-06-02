using AttendanceTracker.Application.RequestHandlers.LoggingHandlers;

namespace AttendanceTracker.Api.Controllers
{
    public class LogController : BaseController
    {
        public LogController(IOrchestrator orchestrator) : base(orchestrator) { }

        [HttpGet("GetAllLoggedResponseTimes")]
        public async Task<IEnumerable<ResponseTimeLog>> GetAllLoggedResponseTimes() => 
            await _orchestrator.GetResponseAsync<GetAllLoggedResponseTimesRequest, IEnumerable<ResponseTimeLog>>(new());

        [HttpGet("GetResponseTimeDetails")]
        public async Task<OverallResponseTimeDetails> GetResponseTimeDetails(GetResponseTimeDetailsRequest request) =>
            await _orchestrator.GetResponseAsync<GetResponseTimeDetailsRequest, OverallResponseTimeDetails>(request);
    }
}
