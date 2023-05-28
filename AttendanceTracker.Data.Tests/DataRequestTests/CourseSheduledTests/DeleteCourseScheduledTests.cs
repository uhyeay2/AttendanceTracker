using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseScheduledTests
{
    public class DeleteCourseScheduledTests : DataTest
    {
        [Fact]
        public async Task DeleteCourseScheduled_Given_CourseScheduledDoesNotExist_ShouldReturn_ZeroRowsAffected()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteCourseScheduled(Guid.NewGuid()));

            Assert.Equal(0, rowsAffected);
        }

        [Fact]
        public async Task DeleteCourseScheduled_Given_CourseScheduledIsDeleted_ShouldReturn_OneRowAffected()
        {
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteCourseScheduled(existingCourseScheduled.Guid));

            Assert.Equal(1, rowsAffected);
        }
    }
}
