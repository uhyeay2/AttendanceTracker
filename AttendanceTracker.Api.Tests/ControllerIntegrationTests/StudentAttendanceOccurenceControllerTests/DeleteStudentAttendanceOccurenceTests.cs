namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentAttendanceOccurenceControllerTests
{
    public class DeleteStudentAttendanceOccurenceTests : BaseStudentAttendanceOccurenceControllerTest
    {
        [Fact]
        public async Task DeleteStudentAttendanceOccurence_Given_StudentAttendanceOccurenceNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.DeleteStudentAttendanceOccurence(Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteStudentAttendanceOccurence_Given_StudentAttendanceOccurenceExists_ShouldDelete_ExistingRecord()
        {
            var existingStudentAttendanceOccurence = await SeedAsync(new SeedStudentAttendanceOccurenceRequest());

            var existsBeforeDeleting = (await _controller.GetStudentAttendanceOccurence(existingStudentAttendanceOccurence.Guid)) != null;

            await _controller.DeleteStudentAttendanceOccurence(existingStudentAttendanceOccurence.Guid);

            var doesNotExistException = await Record.ExceptionAsync(async () => await _controller.GetStudentAttendanceOccurence(existingStudentAttendanceOccurence.Guid));

            Assert.Multiple(() =>
            {
                Assert.True(existsBeforeDeleting);

                Assert.True(doesNotExistException != null);
            });
        }
    }
}
