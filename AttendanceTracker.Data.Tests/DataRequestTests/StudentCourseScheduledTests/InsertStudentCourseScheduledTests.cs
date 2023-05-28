using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Domain.Extensions;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentCourseScheduledTests
{
    public class InsertStudentCourseScheduledTests : DataTest
    {
        [Fact]
        public async Task InsertStudentCourseScheduled_Given_CourseScheduledGuidNotExisting_ShouldReturn_NoRowsUpdated()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentCourseScheduled(existingStudent.StudentCode, courseScheduledGuid: Guid.NewGuid()));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_StudentCodeNotExisting_ShouldReturn_NoRowsUpdated()
        {
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentCourseScheduled(RandomString(), existingCourseScheduled.Guid));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_StudentCodeAndCourseScheduledGuid_ShouldReturn_RowsUpdated()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentCourseScheduled(existingStudent.StudentCode, existingCourseScheduled.Guid));

            await _dataAccess.ExecuteAsync(new DeleteStudentCourseScheduled(existingStudent.StudentCode, existingCourseScheduled.Guid));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_StudentCodeAndCourseScheduledGuidAlreadyExisting_ShouldReturn_NoRowsUpdated()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var existingStudentCourseScheduled = await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode));

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentCourseScheduled(existingStudent.StudentCode, existingStudentCourseScheduled.Guid));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }
    }
}
