using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.InstructorHandlerTests
{
    public class DeleteInstructorHandlerTests : HandlerTest
    {
        private readonly DeleteInstructorHandler _handler;

        public DeleteInstructorHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteInstructor_Given_ExecuteAsync_ReturnsRowsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<DeleteInstructor>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task DeleteInstructor_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndInstructorCodeExists_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<DeleteInstructor>(NoRowsUpdated);
            SetupFetchAsync<IsInstructorCodeExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task DeleteInstructor_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndInstructorCodeNotExists_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<DeleteInstructor>(NoRowsUpdated);
            SetupFetchAsync<IsInstructorCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
