using AttendanceTracker.Api.Controllers;
using AttendanceTracker.Api.Tests.TestHelpers;
using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;
using GenFu;

namespace AttendanceTracker.Api.Tests.IntegrationTests
{
    public class InstructorControllerTests : ControllerTest
    {
        private readonly InstructorController _controller;

        public InstructorControllerTests() => _controller = new(_orchestrator);

        [Fact]
        public async Task InsertInstructor_Given_ValidRequest_Should_InsertInstructor_WithCorrectValues()
        {
            var request = A.New<InsertInstructorRequest>();

            var response = await _controller.InsertInstructor(request);

            await _controller.DeleteInstructor(response.InstructorCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(response);

                Assert.Equal(request.FirstName, response.FirstName);
                Assert.Equal(request.LastName, response.LastName);
            });
        }
    }
}
