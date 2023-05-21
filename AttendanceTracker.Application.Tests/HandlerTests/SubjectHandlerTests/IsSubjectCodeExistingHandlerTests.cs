using AttendanceTracker.Application.RequestHandlers.SubjectHandlers;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.SubjectHandlerTests
{
    public class IsSubjectCodeExistingHandlerTests : HandlerTest
    {
        private readonly IsSubjectCodeExistingHandler _handler;

        public IsSubjectCodeExistingHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task IsSubjectCodeExisting_Given_FetchAsync_ReturnsFalse_Should_ReturnFalse()
        {
            SetupFetchAsync<IsSubjectCodeExisting, bool>(false);

            Assert.False(await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task IsSubjectCodeExisting_Given_FetchAsync_ReturnsTrue_Should_ReturnTrue()
        {
            SetupFetchAsync<IsSubjectCodeExisting, bool>(true);

            Assert.True(await _handler.HandleRequestAsync(new()));
        }
    }
}
