namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseControllerTests
{
    public class IsCourseCodeExistingTests : BaseCourseControllerTest
    {
        [Fact]
        public async Task IsCourseCodeExisting_Given_CourseCodeNotExisting_ShouldReturn_False()
        {
            var result = await _controller.IsCourseCodeExisting(RandomString());

            Assert.False(result);
        }

        [Fact]
        public async Task IsCourseCodeExisting_Given_CourseCodeExisting_ShouldReturn_True()
        {
            var existingCourse = await SeedAsync(new SeedCourseRequest());

            var result = await _controller.IsCourseCodeExisting(existingCourse.CourseCode);

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task IsCourseCodeExisting_Given_InvalidRequest_ShouldThrow_ValidationFailedException(string courseCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.IsCourseCodeExisting(courseCode));
        }
    }
}
