using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class InsertStudentHandlerTests : HandlerTest
    {
        private readonly InsertStudentHandler _handler;

        public InsertStudentHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task InsertStudent_Given_RowIsUpdated_ShouldReturn_StudentInserted()
        {
            var expected = GenFu.GenFu.New<Student_DTO>();

            SetupExecuteAsync<InsertStudent>(OneRowUpdated);
            SetupFetchAsync<GetStudentByCode, Student_DTO>(expected);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.FirstName, result.FirstName);
                Assert.Equal(expected.LastName, result.LastName);
                Assert.Equal(expected.DateOfBirth, result.DateOfBirth);
                Assert.Equal(expected.StudentCode, result.StudentCode);
            });
        }

        [Fact]
        public async Task InsertStudent_Given_NoRowsUpdated_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<InsertStudent>(NoRowsUpdated);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
