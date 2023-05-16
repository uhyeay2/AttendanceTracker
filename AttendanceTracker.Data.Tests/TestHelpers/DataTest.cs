using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.Implementation;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public abstract class DataTest
    {
        static protected readonly IDataAccess _dataAccess = new DataAccess(new DbConnectionFactory(Hidden.TestDatabaseConnectionString));

        protected readonly Guid _testGuid;

        protected readonly string _testString;

        protected readonly RequestFactory _requestFactory;

        public DataTest()
        {
            _testGuid = Guid.NewGuid();

            _testString = _testGuid.ToString();

            _requestFactory = new(_testGuid);
        }
    }
}
