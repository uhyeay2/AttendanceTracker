using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Domain.Extensions;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseTests
{
    public class DeleteCourseTests : DataTest
    {
        [Fact]
        public async Task DeleteCourse_Given_CourseDoesNotExist_ShouldReturn_NoRowsUpdated()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteCourse("CourseNotExisting"));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task DeleteCourse_Given_CourseExists_ShouldReturn_RowsUpdated()
        {
            var course = await _dataSeeder.NewCourse();

            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteCourse(course.CourseCode));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }
    }
}
