using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentCourseScheduledTests
{
    public class DeleteStudentCourseScheduledTests : DataTest
    {
        [Fact]
        public async Task DeleteStudentCourseScheduled_Given_StudentCourseScheduledNotExisting_ShouldReturn_NoRowsUpdated()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudentCourseScheduled(RandomString(), Guid.NewGuid()));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task DeleteStudentCourseScheduled_Given_StudentCourseScheduledIsExisting_ShouldReturn_RowsUpdated()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var existingStudentCourseScheduled = await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode));

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudentCourseScheduled(existingStudent.StudentCode, existingStudentCourseScheduled.Guid));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }
    }
}
