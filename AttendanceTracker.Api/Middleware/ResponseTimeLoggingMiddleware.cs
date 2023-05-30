using AttendanceTracker.Application.RequestHandlers.LoggingHandlers;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace AttendanceTracker.Api.Middleware
{
    [ExcludeFromCodeCoverage]
    public class ResponseTimeLoggingMiddleware : IMiddleware
    {
        private readonly IOrchestrator _orchestrator;

        public ResponseTimeLoggingMiddleware(IOrchestrator orchestrator) => _orchestrator = orchestrator;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var dateTimeRequestWasReceivedInUTC = DateTime.UtcNow;

            var url = context.Request?.Path.Value;

            var stopWatch = new Stopwatch();

            stopWatch.Start();

            await next(context);

            stopWatch.Stop();

            var responseTime = stopWatch.ElapsedMilliseconds;

            await _orchestrator.ExecuteRequestAsync(new LogResponseTimeRequest(dateTimeRequestWasReceivedInUTC, url, responseTime));
        }
    }
}
