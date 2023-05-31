namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentCourseScheduledControllerTests
{
    public class DeleteStudentCourseScheduledTests : BaseStudentCourseScheduledControllerTest
    {
        [Fact]
        public async Task DeleteStudentCourseScheduled_Given_StudentCourseScheduledNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => 
                await _controller.DeleteStudentCourseScheduled(new(studentCode: RandomString(), courseScheduledGuid: Guid.NewGuid())));
        }

        [Fact]
        public async Task DeleteStudentCourseScheduled_Given_StudentCourseScheduledExists_ShouldDelete_ExistingRecord()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());
            var existingStudentCourseScheduled = await SeedAsync(new SeedStudentCourseScheduledRequest(studentCode: existingStudent.StudentCode));

            var existsBeforeDeleting = (await _controller.GetStudentCourseScheduled(new(existingStudent.StudentCode, existingStudentCourseScheduled.Guid))) != null;

            await _controller.DeleteStudentCourseScheduled(new(existingStudent.StudentCode, existingStudentCourseScheduled.Guid));

            var doesNotExistException = await Record.ExceptionAsync(async () => await _controller.GetStudentCourseScheduled(new(existingStudent.StudentCode, existingStudentCourseScheduled.Guid)));

            Assert.Multiple(() =>
            {
                Assert.True(existsBeforeDeleting);

                Assert.True(doesNotExistException != null);
            });
        }
    }
}
