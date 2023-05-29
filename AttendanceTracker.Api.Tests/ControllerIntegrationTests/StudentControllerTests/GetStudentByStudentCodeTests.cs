namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentControllerTests
{
    public class GetStudentByCodeTests : BaseStudentControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task GetStudentByCode_Given_CodeNotProvided_ShouldThrow_ValidationFailedException(string studentCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetStudentByStudentCode(studentCode));
        }

        [Fact]
        public async Task GetStudentByCode_Given_StudentCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.GetStudentByStudentCode(RandomString()));
        }

        [Fact]
        public async Task GetStudentByCode_Given_StudentCodeExisting_ShouldReturn_Student()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var result = await _controller.GetStudentByStudentCode(existingStudent.StudentCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(existingStudent.StudentCode, result.StudentCode);
                Assert.Equal(existingStudent.DateOfBirth, result.DateOfBirth);
                Assert.Equal(existingStudent.FirstName, result.FirstName);
                Assert.Equal(existingStudent.LastName, result.LastName);
            });
        }
    }
}
