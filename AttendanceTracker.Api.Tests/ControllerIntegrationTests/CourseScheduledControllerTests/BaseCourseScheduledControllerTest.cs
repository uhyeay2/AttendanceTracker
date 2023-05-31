namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseScheduledControllerTests
{
    public abstract class BaseCourseScheduledControllerTest : ControllerTest
    {
        protected readonly CourseScheduledController _controller;

        public BaseCourseScheduledControllerTest() => _controller = new(_orchestrator);
    }
}
