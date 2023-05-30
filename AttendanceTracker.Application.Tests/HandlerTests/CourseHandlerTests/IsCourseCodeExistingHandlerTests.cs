using AttendanceTracker.Application.RequestHandlers.CourseHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseHandlerTests
{
    public class IsCourseCodeExistingHandlerTests : HandlerTest
    {
        private readonly IsCourseCodeExistingHandler _handler;

        public IsCourseCodeExistingHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task IsCourseCodeExisting_Given_FetchAsync_ReturnsFalse_Should_ReturnFalse()
        {
            SetupFetchAsync<IsCourseCodeExisting, bool>(false);

            Assert.False(await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task IsCourseCodeExisting_Given_FetchAsync_ReturnsTrue_Should_ReturnTrue()
        {
            SetupFetchAsync<IsCourseCodeExisting, bool>(true);

            Assert.True(await _handler.HandleRequestAsync(new()));
        }
    }
}
