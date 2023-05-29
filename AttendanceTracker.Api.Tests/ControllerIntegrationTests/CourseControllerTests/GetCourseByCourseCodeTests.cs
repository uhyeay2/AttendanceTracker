namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseControllerTests
{
    public class GetCourseByCodeTests : BaseCourseControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task GetCourseByCode_Given_CodeNotProvided_ShouldThrow_ValidationFailedException(string courseCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetCourseByCourseCode(courseCode));
        }

        [Fact]
        public async Task GetCourseByCode_Given_CourseCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.GetCourseByCourseCode(RandomString()));
        }

        [Fact]
        public async Task GetCourseByCode_Given_CourseCodeExisting_ShouldReturn_Course()
        {
            var existingCourse = await SeedAsync(new SeedCourseRequest());

            var result = await _controller.GetCourseByCourseCode(existingCourse.CourseCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(existingCourse.CourseCode, result.CourseCode);
                Assert.Equal(existingCourse.Name, result.Name);
            });
        }
    }
}
