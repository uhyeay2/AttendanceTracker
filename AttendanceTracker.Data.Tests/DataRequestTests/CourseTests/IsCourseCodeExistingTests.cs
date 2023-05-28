using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseTests
{
    public class IsCourseCodeExistingTests : DataTest
    {
        [Fact]
        public async Task IsCourseCodeExisting_Given_CodeNotExisting_ShouldReturn_False()
        {
            Assert.False(await _dataAccess.FetchAsync(new IsCourseCodeExisting(RandomString())));
        }

        [Fact]
        public async Task IsCourseCodeExisting_Given_CodeIsExisting_ShouldReturn_True()
        {
            var course = await SeedAsync(new SeedCourseRequest());

            Assert.True(await _dataAccess.FetchAsync(new IsCourseCodeExisting(course.CourseCode)));
        }
    }
}
