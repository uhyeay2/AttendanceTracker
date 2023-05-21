using AttendanceTracker.Application.RequestHandlers.CourseHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.SubjectRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseHandlerTests
{
    public class InsertCourseHandlerTests : HandlerTest
    {
        private readonly InsertCourseHandler _handler;

        public InsertCourseHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task InsertCourse_Given_CourseIsInserted_ShouldReturn_CourseFetchedAfterInsert()
        {
            var expected = A.New<Course_DTO>();

            SetupFetchAsync<IsSubjectCodeExisting, bool>(true);
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
            SetupFetchAsync<IsSubjectCodeExisting, bool>(true);
            SetupExecuteAsync<InsertCourse>(NoRowsUpdated);

            await Assert.ThrowsAsync<ExpectationFailedException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task InsertCourse_Given_SubjectCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            SetupFetchAsync<IsSubjectCodeExisting, bool>(false);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }
    }
}
