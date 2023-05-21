using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentHandlerTests
{
    public class GetStudentByStudentCodeHandlerTests : HandlerTest
    {
        private readonly GetStudentByStudentCodeHandler _handler;

        public GetStudentByStudentCodeHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetStudentByStudentCode_Given_FetchReturnsStudent_ShouldReturn_StudentFetched()
        {
            var expected = A.New<Student_DTO>();

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
        public async Task GetStudentByStudentCode_Given_FetchReturnsNoStudent_ShouldThrow_NotFoundException()
        {
            SetupFetchAsync<GetStudentByCode, Student_DTO>(null!);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
