using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseTests
{
    public class GetCourseByCourseCodeTests : DataTest
    {
        [Fact]
        public async Task GetCourseByCourseCode_Given_CourseNotExisting_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetCourseByCourseCode(RandomString()));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCourseByCourseCode_Given_CourseIsExisting_Should_ReturnCourse()
        {
            var expected = await GetSeededCourseAsync();

            var result = await _dataAccess.FetchAsync(new GetCourseByCourseCode(expected.CourseCode));

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.Id, result.Id);
                Assert.Equal(expected.Name, result.Name);
                Assert.Equal(expected.CourseCode, result.CourseCode);
            });
        }
    }
}
