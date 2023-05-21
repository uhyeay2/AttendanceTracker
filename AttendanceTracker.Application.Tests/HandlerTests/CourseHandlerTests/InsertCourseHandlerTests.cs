using AttendanceTracker.Application.RequestHandlers.CourseHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseHandlerTests
{
    public class InsertCourseHandlerTests : HandlerTest
    {
        private readonly InsertCourseHandler _handler;

        public InsertCourseHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task InsertCourse_Given_RowIsUpdated_ShouldReturn_CourseInserted()
        {
            var expected = A.New<Course_DTO>();

            SetupExecuteAsync<InsertCourse>(OneRowUpdated);
            SetupFetchAsync<GetCourseByCourseCode, Course_DTO>(expected);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expected.CourseCode, result.CourseCode);
                Assert.Equal(expected.Name, result.Name);
            });
        }

        [Fact]
        public async Task InsertCourse_Given_NoRowsUpdated_ShouldThrow_ExpectationFailedException()
        {
            SetupExecuteAsync<InsertCourse>(NoRowsUpdated);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
