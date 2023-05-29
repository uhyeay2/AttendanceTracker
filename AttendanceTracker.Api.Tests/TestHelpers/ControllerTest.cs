using AttendanceTracker.Application.Abstraction.Interfaces;
using AttendanceTracker.Application.Implementation;
using AttendanceTracker.Data.Implementation;
using AttendanceTracker.Domain.Factories;
using AttendanceTracker.Tests.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace AttendanceTracker.Api.Tests.TestHelpers
{
    public abstract class ControllerTest : IDisposable
    {
        static ControllerTest()
        {
            var services = new ServiceCollection().InjectOrchestration(Hidden.TestDatabaseConnectionString);

            var handlerFactory = new HandlerFactory(services, Application.Implementation.DependencyInjection.GetHandlers());

            _orchestrator = new Orchestrator(handlerFactory);
        }
        public ControllerTest()
        {
            _dataSeeder = new(new DataAccess(new DbConnectionFactory(Hidden.TestDatabaseConnectionString)));
        }

        private readonly DataSeeder _dataSeeder;

        protected static readonly IOrchestrator _orchestrator;

        /// <summary>
        /// Helper for fetching a RandomString to use in tests. Defaults to length of ten. String generated using RandomStringFactory.SharedInstance
        /// </summary>
        protected static string RandomString(int length = 10) => RandomStringFactory.SharedInstance.RandomStringLettersOrNumbers(length);

        protected async Task<TResponse> SeedAsync<TResponse>(DataSeederRequest<TResponse> seedRequest) => await seedRequest.ExecuteAsync(_dataSeeder);

        public void Dispose() => _dataSeeder.Dispose();
    }
}
