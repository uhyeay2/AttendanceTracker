using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.InstructorHandlerTests
{
    public class InsertInstructorHandlerTests : HandlerTest
    {
        private readonly InsertInstructorHandler _handler;

        public InsertInstructorHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task InsertInstructor_Given_RowIsUpdated_ShouldReturn_InstructorInserted()
        {
            var expected = A.New<Instructor_DTO>();

            SetupExecuteAsync<InsertInstructor>(OneRowUpdated);
            SetupFetchAsync<GetInstructorByCode, Instructor_DTO>(expected);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.FirstName, result.FirstName);
                Assert.Equal(expected.LastName, result.LastName);
                Assert.Equal(expected.InstructorCode, result.InstructorCode);
            });
        }

        [Fact]
        public async Task InsertInstructor_Given_NoRowsUpdated_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<InsertInstructor>(NoRowsUpdated);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
