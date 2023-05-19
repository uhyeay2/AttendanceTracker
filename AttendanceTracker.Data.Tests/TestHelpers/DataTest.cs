using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.Implementation;
using AttendanceTracker.Domain.Factories;
using AttendanceTracker.Domain.Interfaces;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public abstract class DataTest : IDisposable
    {
        static protected readonly IDataAccess _dataAccess = new DataAccess(new DbConnectionFactory(Hidden.TestDatabaseConnectionString));

        protected readonly DataSeeder _dataSeeder;

        private readonly IRandomCharacterFactory _randomCharacterFactory;        

        public DataTest()
        {
            _dataSeeder = new(_dataAccess);

            _randomCharacterFactory = new RandomCharacterFactory();
        }

        protected string RandomString(int length = 10) =>
            new(Enumerable.Range(0, length).Select(_ => _randomCharacterFactory.GetRandomLetterOrNumber()).ToArray());

        public void Dispose() => _dataSeeder.PurgeSeededRecords().Wait();
    }
}
