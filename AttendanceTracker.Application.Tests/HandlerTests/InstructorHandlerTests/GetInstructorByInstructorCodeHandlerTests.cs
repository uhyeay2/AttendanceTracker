using AttendanceTracker.Application.RequestHandlers.InstructorHandlers;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.InstructorHandlerTests
{
    public class GetInstructorByInstructorCodeHandlerTests : HandlerTest
    {
        private readonly GetInstructorByInstructorCodeHandler _handler;

        public GetInstructorByInstructorCodeHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetInstructorByInstructorCode_Given_FetchReturnsInstructor_ShouldReturn_InstructorFetched()
        {
            var expected = A.New<Instructor_DTO>();

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
        public async Task GetInstructorByInstructorCode_Given_FetchReturnsNoInstructor_ShouldThrow_NotFoundException()
        {
            SetupFetchAsync<GetInstructorByCode, Instructor_DTO>(null!);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
