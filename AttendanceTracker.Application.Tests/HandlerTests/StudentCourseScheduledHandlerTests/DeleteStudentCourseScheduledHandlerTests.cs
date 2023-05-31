using AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentCourseScheduledHandlerTests
{
    public class DeleteStudentCourseScheduledHandlerTests : HandlerTest
    {
        private readonly DeleteStudentCourseScheduledHandler _handler;

        public DeleteStudentCourseScheduledHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteStudentCourseScheduled_Given_ExecuteAsync_ReturnsRowsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<DeleteStudentCourseScheduled>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task DeleteStudentCourseScheduled_Given_NoRowsUpdated_AndStudentCourseScheduledExists_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<InsertStudentCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCourseScheduledExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task DeleteStudentCourseScheduled_Given_NoRowsUpdated_AndStudentCourseScheduledNotExists_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<InsertStudentCourseScheduled>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCourseScheduledExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
