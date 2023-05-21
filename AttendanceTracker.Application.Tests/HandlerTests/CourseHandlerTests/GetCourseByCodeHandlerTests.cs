using AttendanceTracker.Application.RequestHandlers.CourseHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseHandlerTests
{
    public class GetCourseByCodeHandlerTests : HandlerTest
    {
        private readonly GetCourseByCodeHandler _handler;

        public GetCourseByCodeHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetCourseByCourseCode_Given_FetchReturnsCourse_ShouldReturn_CourseFetched()
        {
            var expected = A.New<Course_DTO>();

            SetupFetchAsync<GetCourseByCourseCode, Course_DTO>(expected);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.Name, result.Name);
                Assert.Equal(expected.CourseCode, result.CourseCode);
            });
        }

        [Fact]
        public async Task GetCourseByCourseCode_Given_FetchReturnsNoCourse_ShouldThrow_NotFoundException()
        {
            SetupFetchAsync<GetCourseByCourseCode, Course_DTO>(null!);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
