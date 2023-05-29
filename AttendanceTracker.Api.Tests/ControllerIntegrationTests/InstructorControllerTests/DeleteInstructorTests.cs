namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.InstructorControllerTests
{
    public class DeleteInstructorTests : BaseInstructorControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task DeleteInstructor_Given_InvalidRequest_ShouldThrow_ValidationFailedException(string instructorCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.DeleteInstructor(instructorCode));
        }

        [Fact]
        public async Task DeleteInstructor_Given_InstructorNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.DeleteInstructor(RandomString()));
        }

        [Fact]
        public async Task DeleteInstructor_Given_InstructorIsExisting_ShouldDelete_ExistingInstructor()
        {
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var existsBeforeDeleting = await _controller.IsInstructorCodeExisting(existingInstructor.InstructorCode);

            await _controller.DeleteInstructor(existingInstructor.InstructorCode);

            var existsAfterDeleting = await _controller.IsInstructorCodeExisting(existingInstructor.InstructorCode);

            Assert.Multiple(() =>
            {
                Assert.True(existsBeforeDeleting);

                Assert.False(existsAfterDeleting);
            });
        }
    }
}
