using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class IsStudentCodeExistingHandlerTests : HandlerTest
    {
        private readonly IsStudentCodeExistingHandler _handler;

        public IsStudentCodeExistingHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task IsStudentCodeExisting_Given_FetchAsync_ReturnsFalse_Should_ReturnFalse()
        {
            SetupFetchAsync<IsStudentCodeExisting, bool>(false);

            Assert.False(await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task IsStudentCodeExisting_Given_FetchAsync_ReturnsTrue_Should_ReturnTrue()
        {
            SetupFetchAsync<IsStudentCodeExisting, bool>(true);

            Assert.True(await _handler.HandleRequestAsync(new()));
        }
    }
}
