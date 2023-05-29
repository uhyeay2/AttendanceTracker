namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.SubjectControllerTests
{
    public abstract class BaseSubjectControllerTest : ControllerTest
    {
        protected readonly SubjectController _controller;

        public BaseSubjectControllerTest() => _controller = new(_orchestrator);
    }
}
