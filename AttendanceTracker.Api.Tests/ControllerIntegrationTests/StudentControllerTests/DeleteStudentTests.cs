namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentControllerTests
{
    public class DeleteStudentTests : BaseStudentControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task DeleteStudent_Given_InvalidRequest_ShouldThrow_ValidationFailedException(string studentCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.DeleteStudent(studentCode));
        }

        [Fact]
        public async Task DeleteStudent_Given_StudentNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.DeleteStudent(RandomString()));
        }

        [Fact]
        public async Task DeleteStudent_Given_StudentIsExisting_ShouldDelete_ExistingStudent()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var existsBeforeDeleting = await _controller.IsStudentCodeExisting(existingStudent.StudentCode);

            await _controller.DeleteStudent(existingStudent.StudentCode);

            var existsAfterDeleting = await _controller.IsStudentCodeExisting(existingStudent.StudentCode);

            Assert.Multiple(() =>
            {
                Assert.True(existsBeforeDeleting);

                Assert.False(existsAfterDeleting);
            });
        }
    }
}
