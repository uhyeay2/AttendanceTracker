using AttendanceTracker.Application.RequestHandlers.SubjectHandlers;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.SubjectHandlerTests
{
    public class GetAllSubjectsHandlerTests : HandlerTest
    {
        private readonly GetAllSubjectsHandler _handler;

        public GetAllSubjectsHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetAllSubjects_Given_FetchReturnsSubjects_ShouldReturn_SubjectsFetched()
        {
            var expected = A.ListOf<Subject_DTO>();

            SetupFetchListAsync<GetAllSubjects, Subject_DTO>(expected);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.Equal(expected.Count, result.Count());

                Assert.True(expected.All(_ => result.Select(r => r.SubjectCode).Contains(_.SubjectCode)));
            });
        }

        [Fact]
        public async Task GetAllSubjects_Given_FetchReturnsNoStudents_ShouldReturn_EmptyEnumerable()
        {
            SetupFetchListAsync<GetAllSubjects, Subject_DTO>(Enumerable.Empty<Subject_DTO>());

            var result = await _handler.HandleRequestAsync(new());

            Assert.Empty(result);
        }
    }
}
