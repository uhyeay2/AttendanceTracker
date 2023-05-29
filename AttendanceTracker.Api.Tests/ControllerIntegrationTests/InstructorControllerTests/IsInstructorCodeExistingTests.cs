namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.InstructorControllerTests
{
    public class IsInstructorCodeExistingTests : BaseInstructorControllerTest
    {
        [Fact]
        public async Task IsInstructorCodeExisting_Given_InstructorCodeNotExisting_ShouldReturn_False()
        {
            var result = await _controller.IsInstructorCodeExisting(RandomString());

            Assert.False(result);
        }

        [Fact]
        public async Task IsInstructorCodeExisting_Given_InstructorCodeExisting_ShouldReturn_True()
        {
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var result = await _controller.IsInstructorCodeExisting(existingInstructor.InstructorCode);

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task IsInstructorCodeExisting_Given_InvalidRequest_ShouldThrow_ValidationFailedException(string instructorCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.IsInstructorCodeExisting(instructorCode));
        }
    }
}
