using AttendanceTracker.Application.RequestHandlers.SubjectHandlers;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.SubjectHandlerTests
{
    public class DeleteSubjectHandlerTests : HandlerTest
    {
        private readonly DeleteSubjectHandler _handler;

        public DeleteSubjectHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task DeleteSubject_Given_ExecuteAsync_ReturnsRowsUpdated_ShouldNot_ThrowException()
        {
            SetupExecuteAsync<DeleteSubject>(OneRowUpdated);

            Assert.Null(await Record.ExceptionAsync(async () => await _handler.HandleRequestAsync(new())));
        }

        [Fact]
        public async Task DeleteSubject_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndSubjectCodeExists_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<DeleteSubject>(NoRowsUpdated);
            SetupFetchAsync<IsSubjectCodeExisting, bool>(true);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task DeleteSubject_Given_ExecuteAsync_ReturnsNoRowsUpdated_AndSubjectCodeNotExists_ShouldThrow_DoesNotExistException()
        {
            SetupExecuteAsync<DeleteSubject>(NoRowsUpdated);
            SetupFetchAsync<IsSubjectCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
