namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseControllerTests
{
    public abstract class BaseCourseControllerTest : ControllerTest
    {
        protected readonly CourseController _controller;

        public BaseCourseControllerTest() => _controller = new(_orchestrator);
    }
}
