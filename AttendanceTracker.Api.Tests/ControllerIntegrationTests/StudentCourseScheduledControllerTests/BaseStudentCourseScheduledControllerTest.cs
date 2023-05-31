namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentCourseScheduledControllerTests
{
    public abstract class BaseStudentCourseScheduledControllerTest : ControllerTest
    {
        protected readonly StudentCourseScheduledController _controller;

        public BaseStudentCourseScheduledControllerTest() => _controller = new(_orchestrator);
    }
}
