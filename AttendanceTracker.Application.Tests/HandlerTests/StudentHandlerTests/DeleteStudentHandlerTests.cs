using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class DeleteStudentHandlerTests : HandlerTest
    {
        private readonly DeleteStudentHandler _handler;

        public DeleteStudentHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteStudent_Given_ExecuteAsync_ReturnsRowsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<DeleteStudent>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task DeleteStudent_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndStudentCodeExists_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<DeleteStudent>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCodeExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task DeleteStudent_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndStudentCodeNotExists_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<DeleteStudent>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
