using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Models;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentCourseScheduledHandlerTests
{
    public class GetAllStudentCourseScheduledByStudentCodeHandlerTests : HandlerTest
    {
        private readonly GetAllStudentCourseScheduledByStudentCodeHandler _handler;

        public GetAllStudentCourseScheduledByStudentCodeHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task GetAllStudentCourseScheduled_Given_NoCoursesFoundByStudentCode_ShouldThrow_DoesNotExistException()
        {
            SetupFetchListAsync<GetAllStudentCourseScheduledByStudentCode, CourseScheduled_DTO>(Enumerable.Empty<CourseScheduled_DTO>());

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task GetAllStudentCourseScheduled_Given_CoursesFoundWithStudentCode_ShouldReturn_StudentWithCoursesFound()
        {
            var expectedStudentDTO = A.New<Student_DTO>();
            var expectedCourseScheduled = A.New<CourseScheduled>();

            SetupFetchAsync<GetStudentByCode, Student_DTO>(expectedStudentDTO);
            SetupFetchListAsync<GetAllStudentCourseScheduledByStudentCode, CourseScheduled_DTO>(A.ListOf<CourseScheduled_DTO>());
            SetupGetResponseAsync<GetCourseScheduledByGuidRequest, CourseScheduled>(expectedCourseScheduled);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expectedStudentDTO.FirstName, result.Student.FirstName);
                Assert.Equal(expectedStudentDTO.LastName, result.Student.LastName);
                Assert.Equal(expectedStudentDTO.DateOfBirth, result.Student.DateOfBirth);
                Assert.Equal(expectedStudentDTO.StudentCode, result.Student.StudentCode);

                Assert.All(result.CoursesScheduled, c => c.Equals(expectedCourseScheduled));
            });
        }
    }
}
