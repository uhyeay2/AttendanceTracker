using AttendanceTracker.Application.Abstraction.Interfaces;
using AttendanceTracker.Application.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace AttendanceTracker.Api.Tests.TestHelpers
{
    public abstract class ControllerTest
    {

        protected static readonly IOrchestrator _orchestrator;

        static ControllerTest()
        {
            var services = new ServiceCollection().InjectOrchestration(Hidden.TestDatabaseConnectionString);

            var handlerFactory = new HandlerFactory(services, DependencyInjection.GetHandlers());

            _orchestrator = new Orchestrator(handlerFactory);
        }
    }
}
