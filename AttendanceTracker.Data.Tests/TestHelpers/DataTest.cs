using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.Implementation;
using AttendanceTracker.Domain.Factories;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public abstract class DataTest : IDisposable
    {
        public DataTest()
        {
            _dataAccess = new DataAccess(new DbConnectionFactory(Hidden.TestDatabaseConnectionString));

            _dataSeeder = new(_dataAccess);
        }
        
        protected readonly IDataAccess _dataAccess;

        private readonly DataSeeder _dataSeeder;

        /// <summary>
        /// Helper for fetching a RandomString to use in tests. Defaults to length of ten. String generated using RandomStringFactory.SharedInstance
        /// </summary>
        protected static string RandomString(int length = 10) => RandomStringFactory.SharedInstance.RandomStringLettersOrNumbers(length);

        protected async Task<TResponse> SeedAsync<TResponse>(DataSeederRequest<TResponse> seedRequest) => await seedRequest.ExecuteAsync(_dataSeeder);

        public void Dispose() => _dataSeeder.PurgeSeededRecordsAsync().ConfigureAwait(true);
    }
}
