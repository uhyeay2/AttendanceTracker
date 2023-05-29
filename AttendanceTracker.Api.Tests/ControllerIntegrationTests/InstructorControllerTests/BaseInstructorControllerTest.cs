namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.InstructorControllerTests
{
    public abstract class BaseInstructorControllerTest : ControllerTest
    {
        protected readonly InstructorController _controller;

        public BaseInstructorControllerTest() => _controller = new(_orchestrator);
    }
}
