using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class UpdateStudentHandlerTests : HandlerTest
    {
        private readonly UpdateStudentHandler _handler;

        public UpdateStudentHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task UpdateStudent_Given_RowIsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<UpdateStudent>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task UpdateStudent_Given_NoRowsUpdated_And_StudentCodeNotTaken_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<UpdateStudent>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCodeTaken, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task UpdateStudent_Given_NoRowsUpdated_And_StudentCodeIsTaken_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<UpdateStudent>(NoRowsUpdated);
            SetupFetchAsync<IsStudentCodeTaken, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
