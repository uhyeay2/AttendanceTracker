namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseControllerTests
{
    public class DeleteCourseTests : BaseCourseControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task DeleteCourse_Given_InvalidRequest_ShouldThrow_ValidationFailedException(string courseCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.DeleteCourse(courseCode));
        }

        [Fact]
        public async Task DeleteCourse_Given_CourseCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.DeleteCourse(RandomString()));
        }

        [Fact]
        public async Task DeleteCourse_Given_CourseCodeIsExisting_ShouldDelete_ExistingCourse()
        {
            var existingCourse = await SeedAsync(new SeedCourseRequest());

            var existsBeforeDeleting = await _controller.IsCourseCodeExisting(existingCourse.CourseCode);

            await _controller.DeleteCourse(existingCourse.CourseCode);

            var existsAfterDeleting = await _controller.IsCourseCodeExisting(existingCourse.CourseCode);

            Assert.Multiple(() =>
            {
                Assert.True(existsBeforeDeleting);

                Assert.False(existsAfterDeleting);
            });
        }
    }
}
