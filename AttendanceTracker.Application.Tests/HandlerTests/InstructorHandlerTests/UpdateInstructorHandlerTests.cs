using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.InstructorHandlerTests
{
    public class UpdateInstructorHandlerTests : HandlerTest
    {
        private readonly UpdateInstructorHandler _handler;

        public UpdateInstructorHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task UpdateInstructor_Given_RowIsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<UpdateInstructor>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task UpdateInstructor_Given_NoRowsUpdated_And_InstructorCodeNotTaken_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<UpdateInstructor>(NoRowsUpdated);
            SetupFetchAsync<IsInstructorCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task UpdateInstructor_Given_NoRowsUpdated_And_InstructorCodeIsTaken_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<UpdateInstructor>(NoRowsUpdated);
            SetupFetchAsync<IsInstructorCodeExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
