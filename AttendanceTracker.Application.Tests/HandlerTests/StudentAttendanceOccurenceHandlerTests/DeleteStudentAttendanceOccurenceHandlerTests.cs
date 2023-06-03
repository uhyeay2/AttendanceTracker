using AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentAttendanceOccurenceHandlerTests
{
    public class DeleteStudentAttendanceOccurenceHandlerTests : HandlerTest
    {
        private readonly DeleteStudentAttendanceOccurenceHandler _handler;

        public DeleteStudentAttendanceOccurenceHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteStudentAttendanceOccurence_Given_ExecuteAsync_ReturnsRowsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<DeleteStudentAttendanceOccurence>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task DeleteStudentAttendanceOccurence_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndStudentAttendanceOccurenceExists_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<DeleteStudentAttendanceOccurence>(NoRowsUpdated);
            SetupFetchAsync<IsStudentAttendanceOccurenceExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task DeleteStudentAttendanceOccurence_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndStudentAttendanceOccurenceNotExists_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<DeleteStudentAttendanceOccurence>(NoRowsUpdated);
            SetupFetchAsync<IsStudentAttendanceOccurenceExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
