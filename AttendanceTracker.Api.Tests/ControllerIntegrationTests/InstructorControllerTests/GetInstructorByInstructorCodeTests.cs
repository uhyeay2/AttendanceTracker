namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.InstructorControllerTests
{
    public class GetInstructorByCodeTests : BaseInstructorControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task GetInstructorByCode_Given_CodeNotProvided_ShouldThrow_ValidationFailedException(string instructorCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetInstructorByInstructorCode(instructorCode));
        }

        [Fact]
        public async Task GetInstructorByCode_Given_InstructorCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.GetInstructorByInstructorCode(RandomString()));
        }

        [Fact]
        public async Task GetInstructorByCode_Given_InstructorCodeExisting_ShouldReturn_Instructor()
        {
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var result = await _controller.GetInstructorByInstructorCode(existingInstructor.InstructorCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(existingInstructor.InstructorCode, result.InstructorCode);
                Assert.Equal(existingInstructor.FirstName, result.FirstName);
                Assert.Equal(existingInstructor.LastName, result.LastName);
            });
        }
    }
}
