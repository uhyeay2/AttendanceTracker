namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseScheduledControllerTests
{
    public class DeleteCourseScheduledTests : BaseCourseScheduledControllerTest
    {
        [Fact]
        public async Task DeleteCourseScheduled_Given_EmptyGuid_ShouldThrow_ValidationFailedException()
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.DeleteCourseScheduled(Guid.Empty));
        }

        [Fact]
        public async Task DeleteCourseScheduled_Given_CourseScheduledNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.DeleteCourseScheduled(Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteCourseScheduled_Given_CourseScheduledIsExisting_ShouldDelete_ExistingCourseScheduled()
        {
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var existsBeforeDeleting = (await _controller.GetCourseScheduledByGuid(existingCourseScheduled.Guid)) != null;

            await _controller.DeleteCourseScheduled(existingCourseScheduled.Guid);

            var doesNotExistException = await Record.ExceptionAsync(async () => await _controller.GetCourseScheduledByGuid(existingCourseScheduled.Guid));

            Assert.Multiple(() =>
            {
                Assert.True(existsBeforeDeleting);

                Assert.NotNull(doesNotExistException);
            });
        }
    }
}
