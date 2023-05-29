using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;

namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.InstructorControllerTests
{
    public class UpdateInstructorTests : BaseInstructorControllerTest
    {
        [Fact]
        public async Task UpdateInstructor_Given_InstructorNotExisting_ShouldThrow_DoesNotExistException()
        {
            var updateRequest = new UpdateInstructorRequest(instructorCode: RandomString());
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.UpdateInstructor(updateRequest));
        }

        [Fact]
        public async Task UpdateInstructor_Given_InstructorIsExisting_ShouldThrow_NoException()
        {
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var updateRequest = new UpdateInstructorRequest(instructorCode: existingInstructor.InstructorCode);

            var exception = await Record.ExceptionAsync(async () => await _controller.UpdateInstructor(updateRequest));

            Assert.Null(exception);
        }

        [Fact]
        public async Task UpdateInstructor_Given_InstructorIsExisting_ShouldUpdate_InstructorWithMatchingInstructorCode()
        {
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var expected = new UpdateInstructorRequest(existingInstructor.InstructorCode, RandomString(), RandomString());

            await _controller.UpdateInstructor(expected);

            var instructorAfterUpdate = await _controller.GetInstructorByInstructorCode(existingInstructor.InstructorCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(instructorAfterUpdate);

                Assert.Equal(existingInstructor.InstructorCode, instructorAfterUpdate.InstructorCode);
                Assert.Equal(expected.FirstName, instructorAfterUpdate.FirstName);
                Assert.Equal(expected.LastName, instructorAfterUpdate.LastName);
            });
        }
    }
}
