using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.Implementation;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public abstract class DataTest : IDisposable
    {
        static protected readonly IDataAccess _dataAccess = new DataAccess(new DbConnectionFactory(Hidden.TestDatabaseConnectionString));

        protected readonly DataSeeder _dataSeeder;

        public DataTest()
        {
            _dataSeeder = new(_dataAccess);
        }

        public void Dispose() => _dataSeeder.PurgeSeededRecords().Wait();
    }
}
