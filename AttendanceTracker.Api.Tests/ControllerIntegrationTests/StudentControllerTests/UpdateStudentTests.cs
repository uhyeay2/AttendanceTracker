using AttendanceTracker.Application.RequestHandlers.StudentHandlers;

namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentControllerTests
{
    public class UpdateStudentTests : BaseStudentControllerTest
    {
        [Fact]
        public async Task UpdateStudent_Given_StudentNotExisting_ShouldThrow_DoesNotExistException()
        {
            var updateRequest = new UpdateStudentRequest(studentCode: RandomString());
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.UpdateStudent(updateRequest));
        }

        [Fact]
        public async Task UpdateStudent_Given_StudentIsExisting_ShouldThrow_NoException()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var updateRequest = new UpdateStudentRequest(studentCode: existingStudent.StudentCode);

            var exception = await Record.ExceptionAsync(async() => await _controller.UpdateStudent(updateRequest));

            Assert.Null(exception);
        }

        [Fact]
        public async Task UpdateStudent_Given_StudentIsExisting_ShouldUpdate_StudentWithMatchingStudentCode()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var expected = new UpdateStudentRequest(existingStudent.StudentCode, RandomString(), RandomString(), existingStudent.DateOfBirth.AddDays(10));

            await _controller.UpdateStudent(expected);

            var studentAfterUpdate = await _controller.GetStudentByStudentCode(existingStudent.StudentCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(studentAfterUpdate);

                Assert.Equal(existingStudent.StudentCode, studentAfterUpdate.StudentCode);
                Assert.Equal(expected.DateOfBirth, studentAfterUpdate.DateOfBirth);
                Assert.Equal(expected.FirstName, studentAfterUpdate.FirstName);
                Assert.Equal(expected.LastName, studentAfterUpdate.LastName);
            });
        }
    }
}
