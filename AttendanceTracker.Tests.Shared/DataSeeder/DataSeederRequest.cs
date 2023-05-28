using AttendanceTracker.Domain.Factories;
using AttendanceTracker.Domain.Interfaces;

namespace AttendanceTracker.Tests.Shared.DataSeeder
{
    public abstract class DataSeederRequest<TDataToFetch>
    {
        /// <summary>
        /// This static member is shared by all DataSeederRequests for generating random strings for dummy data.
        /// </summary>
        protected static readonly IRandomStringFactory _randomStringFactory = RandomStringFactory.SharedInstance;

        /// <summary>
        /// This abstrtract method allows a DataSeederRequest to define how it uses the DataSeeder to process its request.
        /// </summary>
        public abstract Task<TDataToFetch> ExecuteAsync(DataSeeder dataSeeder);
    }
}
