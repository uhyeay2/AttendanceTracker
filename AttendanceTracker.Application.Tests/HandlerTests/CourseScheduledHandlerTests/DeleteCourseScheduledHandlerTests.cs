using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseScheduledHandlerTests
{
    public class DeleteCourseScheduledHandlerTests : HandlerTest
    {
        private readonly DeleteCourseScheduledHandler _handler;

        public DeleteCourseScheduledHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteCourseScheduled_Given_ExecuteAsync_ReturnsRowsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<DeleteCourseScheduled>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task DeleteCourseScheduled_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndCourseScheduledGuidExists_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<DeleteCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsCourseScheduledGuidExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task DeleteCourseScheduled_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndCourseScheduledGuidNotExists_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<DeleteCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsCourseScheduledGuidExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}