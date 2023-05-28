
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseTests
{
    public class IsCourseScheduledGuidExistingTests : DataTest
    {
        [Fact]
        public async Task IsCourseScheduledGuidExisting_Given_GuidNotExisting_ShouldReturn_False()
        {
            Assert.False(await _dataAccess.FetchAsync(new IsCourseScheduledGuidExisting(Guid.NewGuid())));
        }

        [Fact]
        public async Task IsCourseScheduledGuidExisting_Given_GuidIsExisting_ShouldReturn_True()
        {
            var courseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            Assert.True(await _dataAccess.FetchAsync(new IsCourseScheduledGuidExisting(courseScheduled.Guid)));
        }
    }
}
