using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Domain.Extensions;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseTests
{
    public class UpdateCourseTests : DataTest
    {
        [Fact]
        public async Task UpdateCourse_Given_NoCourseExists_ShouldReturn_NoRowsUpdated()
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateCourse(RandomString()));

            Assert.True(rowsAffected.NoRowsAreUpdated());
        }

        [Fact]
        public async Task UpdateCourse_Given_CourseExists_ShouldReturn_RowsUpdated()
        {
            var course = await SeedAsync(new SeedCourseRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateCourse(course.CourseCode, course.Name));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }

        [Fact]
        public async Task UpdateCourse_Given_Name_ShouldUpdate_DatabaseRecord()
        {
            var expectedName = "New Course Name";

            var course = await SeedAsync(new SeedCourseRequest());

            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateCourse(course.CourseCode, expectedName));

            Assert.True(rowsAffected.AnyRowsAreUpdated());
        }
    }
}
