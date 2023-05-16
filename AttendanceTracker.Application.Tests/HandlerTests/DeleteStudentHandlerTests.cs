using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Exceptions;

namespace AttendanceTracker.Application.Tests.HandlerTests
{
    public class DeleteStudentHandlerTests : HandlerTest
    {
        private readonly DeleteStudentHandler _handler;

        public DeleteStudentHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteStudent_Given_ExecuteAsync_ReturnsRowsUpdated_Should_ThrowNoException()
        {
            SetupExecuteAsync<DeleteStudent>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task DeleteStudent_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndStudentCodeExists_Should_ThrowExpectationFailedException()
        {
            SetupExecuteAsync<DeleteStudent>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCodeTaken, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task DeleteStudent_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndStudentCodeNotExists_Should_ThrowDoesNotExistException()
        {
            SetupExecuteAsync<DeleteStudent>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCodeTaken, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
