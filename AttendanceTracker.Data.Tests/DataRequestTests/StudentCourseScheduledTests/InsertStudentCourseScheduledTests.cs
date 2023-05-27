using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Domain.Extensions;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentCourseScheduledTests
{
    public class InsertStudentCourseScheduledTests : DataTest
    {
        [Fact]
        public async Task InsertStudentCourseScheduled_Given_CourseScheduledGuidNotExisting_ShouldReturn_NoRowsUpdated()
        {
            var existingStudent = await GetSeededStudentAsync();

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentCourseScheduled(existingStudent.StudentCode, courseScheduledGuid: Guid.NewGuid()));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_StudentCodeNotExisting_ShouldReturn_NoRowsUpdated()
        {
            var existingCourseScheduled = await GetSeededCourseScheduledAsync();

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentCourseScheduled(RandomString(), existingCourseScheduled.Guid));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_ExistingStudentCodeAndCourseScheduledGuid_ShouldReturn_RowsUpdated()
        {
            var existingStudent = await GetSeededStudentAsync();
            var existingCourseScheduled = await GetSeededCourseScheduledAsync();

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudentCourseScheduled(existingStudent.StudentCode, existingCourseScheduled.Guid));

            await _dataAccess.ExecuteAsync(new DeleteStudentCourseScheduled(existingStudent.StudentCode, existingCourseScheduled.Guid));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }
    }
}
