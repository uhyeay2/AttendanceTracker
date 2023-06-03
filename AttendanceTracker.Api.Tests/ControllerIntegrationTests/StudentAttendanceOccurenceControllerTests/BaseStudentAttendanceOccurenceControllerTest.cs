namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentAttendanceOccurenceControllerTests
{
    public abstract class BaseStudentAttendanceOccurenceControllerTest : ControllerTest
    {
        protected readonly StudentAttendanceOccurenceController _controller;

        public BaseStudentAttendanceOccurenceControllerTest() => _controller = new(_orchestrator);
    }
}
