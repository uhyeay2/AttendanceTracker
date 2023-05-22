using AttendanceTracker.Data.Abstraction.Interfaces;
using AttendanceTracker.Data.DataTransferObjects;
using AttendanceTracker.Data.Implementation;
using AttendanceTracker.Domain.Constants;
using AttendanceTracker.Domain.Factories;
using AttendanceTracker.Domain.Interfaces;

namespace AttendanceTracker.Data.Tests.TestHelpers
{
    public abstract class DataTest : IDisposable
    {
        protected readonly IDataAccess _dataAccess = new DataAccess(new DbConnectionFactory(Hidden.TestDatabaseConnectionString));

        private readonly DataSeeder _dataSeeder;

        private readonly IRandomCharacterFactory _randomCharacterFactory = new RandomCharacterFactory();

        public DataTest() => _dataSeeder = new(_dataAccess);

        /// <summary>
        /// Return a RandomString of the specified length for using in tests
        /// </summary>
        protected string RandomString(int length = 10) =>
            new(Enumerable.Range(0, length).Select(_ => _randomCharacterFactory.GetRandomLetterOrNumber()).ToArray());

        protected async Task<Course_DTO> GetSeededCourseAsync(string? subjectCode = null, string ? courseCode = null, string? name = null) =>
            await _dataSeeder.NewCourseAsync(courseCode ?? RandomString(CourseCodeConstants.MaxLength), subjectCode ?? (await GetSeededSubjectAsync()).SubjectCode, name ?? RandomString());

        protected async Task<Student_DTO> GetSeededStudentAsync(string? studentCode = null, string? firstName = null, string? lastName = null, DateTime? dateOfBirth = null) =>
            await _dataSeeder.NewStudentAsync(studentCode ?? RandomString(StudentCodeConstants.ExpectedLength), firstName ?? RandomString(), lastName ?? RandomString(), dateOfBirth ?? DateTime.Now);

        protected async Task<Subject_DTO> GetSeededSubjectAsync(string? subjectCode = null, string? name = null) =>
            await _dataSeeder.NewSubjectAsync(subjectCode ?? RandomString(), name ?? RandomString());

        public void Dispose() => _dataSeeder.PurgeSeededRecords().ConfigureAwait(true);
    }
}
