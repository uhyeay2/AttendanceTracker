using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Domain.Models;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentCourseScheduledHandlerTests
{
    public class GetStudentCourseScheduledHandlerTests : HandlerTest
    {
        private readonly GetStudentCourseScheduledHandler _handler;

        public GetStudentCourseScheduledHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task GetStudentCourseScheduled_Given_NoCourseScheduledWithStudentCodeAndGuidProvided_ShouldThrow_DoesNotExistException()
        {
            SetupFetchAsync<GetStudentCourseScheduled, CourseScheduled_DTO>(null!);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task GetStudentCourseScheduled_Given_CourseScheduledExists_ShouldReturn_StudentAndCourseScheduledFetchedWithOrchestrator()
        {
            var expectedStudent = A.New<Student>();
            var expectedCourseScheduled = A.New<CourseScheduled>();
            
            SetupFetchAsync<GetStudentCourseScheduled, CourseScheduled_DTO>(A.New<CourseScheduled_DTO>());
            SetupGetResponseAsync<GetStudentByStudentCodeRequest, Student>(expectedStudent);
            SetupGetResponseAsync<GetCourseScheduledByGuidRequest, CourseScheduled>(expectedCourseScheduled);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expectedStudent, result.Student);
                Assert.Equal(expectedCourseScheduled, result.CourseScheduled);
            });
        }
    }
}
