using AttendanceTracker.Application.RequestHandlers.SubjectHandlers;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.SubjectHandlerTests
{
    public class InsertSubjectHandlerTests : HandlerTest
    {
        private readonly InsertSubjectHandler _handler;

        public InsertSubjectHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task InsertSubject_Given_RowIsUpdated_ShouldReturn_SubjectInserted()
        {
            var expected = A.New<Subject_DTO>();

            SetupExecuteAsync<InsertSubject>(OneRowUpdated);
            SetupFetchAsync<GetSubjectByCode, Subject_DTO>(expected);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.SubjectCode, result.SubjectCode);
                Assert.Equal(expected.Name, result.Name);
            });
        }

        [Fact]
        public async Task InsertSubject_Given_NoRowsUpdated_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<InsertSubject>(NoRowsUpdated);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
