using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.InstructorHandlerTests
{
    public class IsInstructorCodeExistingHandlerTests : HandlerTest
    {
        private readonly IsInstructorCodeExistingHandler _handler;

        public IsInstructorCodeExistingHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task IsInstructorCodeExisting_Given_FetchAsync_ReturnsFalse_Should_ReturnFalse()
        {
            SetupFetchAsync<IsInstructorCodeExisting, bool>(false);

            Assert.False(await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task IsInstructorCodeExisting_Given_FetchAsync_ReturnsTrue_Should_ReturnTrue()
        {
            SetupFetchAsync<IsInstructorCodeExisting, bool>(true);

            Assert.True(await _handler.HandleRequestAsync(new()));
        }
    }
}
