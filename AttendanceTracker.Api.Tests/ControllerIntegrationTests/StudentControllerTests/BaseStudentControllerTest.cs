namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentControllerTests
{
    public abstract class BaseStudentControllerTest : ControllerTest
    {
        protected readonly StudentController _controller;

        public BaseStudentControllerTest() => _controller = new(_orchestrator);
    }
}
