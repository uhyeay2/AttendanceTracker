namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentControllerTests
{
    public class IsStudentCodeExistingTests : BaseStudentControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task IsStudentCodeExisting_Given_InvalidRequest_ShouldThrow_ValidationFailedException(string studentCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.IsStudentCodeExisting(studentCode));
        }

        [Fact]
        public async Task IsStudentCodeExisting_Given_StudentCodeNotExisting_ShouldReturn_False()
        {
            var result = await _controller.IsStudentCodeExisting(RandomString());

            Assert.False(result);
        }

        [Fact]
        public async Task IsStudentCodeExisting_Given_StudentCodeExisting_ShouldReturn_True()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var result = await _controller.IsStudentCodeExisting(existingStudent.StudentCode);

            Assert.True(result);
        }
    }
}
