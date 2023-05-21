using AttendanceTracker.Application.RequestHandlers.SubjectHandlers;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.SubjectHandlerTests
{
    public class GetSubjectByCodeHandlerTests : HandlerTest
    {
        private readonly GetSubjectByCodeHandler _handler;

        public GetSubjectByCodeHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetSubjectBySubjectCode_Given_FetchReturnsSubject_ShouldReturn_SubjectFetched()
        {
            var expected = A.New<Subject_DTO>();

            SetupFetchAsync<GetSubjectByCode, Subject_DTO>(expected);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.Name, result.Name);
                Assert.Equal(expected.SubjectCode, result.SubjectCode);
            });
        }

        [Fact]
        public async Task GetSubjectBySubjectCode_Given_FetchReturnsNoSubject_ShouldThrow_NotFoundException()
        {
            SetupFetchAsync<GetSubjectByCode, Subject_DTO>(null!);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
