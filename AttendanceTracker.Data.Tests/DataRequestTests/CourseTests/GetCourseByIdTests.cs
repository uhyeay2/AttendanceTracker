using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.CourseTests
{
    public class GetCourseByIdTests : DataTest
    {
        [Fact]
        public async Task GetCourseById_Given_CourseIdNotExisting_ShouldReturn_Null()
        {
            var result = await _dataAccess.FetchAsync(new GetCourseById(int.MaxValue));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCourseById_Given_CourseIsExisting_Should_ReturnCourse()
        {
            var expected = await GetSeededCourseAsync();

            var result = await _dataAccess.FetchAsync(new GetCourseById(expected.Id));

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.Id, result.Id);
                Assert.Equal(expected.Name, result.Name);
                Assert.Equal(expected.Id, result.Id);
            });
        }
    }
}
