
using AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class IsStudentAttendanceOccurenceExistingHandlerTests : HandlerTest
    {
        private readonly IsStudentAttendanceOccurenceExistingHandler _handler;

        public IsStudentAttendanceOccurenceExistingHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task IsStudentAttendanceOccurenceExisting_Given_FetchAsync_ReturnsFalse_Should_ReturnFalse()
        {
            SetupFetchAsync<IsStudentAttendanceOccurenceExisting, bool>(false);

            Assert.False(await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task IsStudentAttendanceOccurenceExisting_Given_FetchAsync_ReturnsTrue_Should_ReturnTrue()
        {
            SetupFetchAsync<IsStudentAttendanceOccurenceExisting, bool>(true);

            Assert.True(await _handler.HandleRequestAsync(new()));
        }
    }
}
