using AttendanceTracker.Application.Abstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracker.Api.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IOrchestrator _orchestrator;

        public BaseController(IOrchestrator orchestrator) => _orchestrator = orchestrator; 
    }
}
