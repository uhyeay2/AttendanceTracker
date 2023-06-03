using AttendanceTracker.Application.RequestHandlers.StudentCourseScheduledHandlers;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
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
            SetupFetchAsync<GetStudentCourseScheduled, StudentCourseScheduled_DTO>(null!);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task GetStudentCourseScheduled_Given_CourseScheduledExists_ShouldReturn_StudentAndCourseScheduledFetchedWithOrchestrator()
        {
            var expectedStudent = A.New<Student>();
            SetupGetResponseAsync<GetStudentByStudentCodeRequest, Student>(expectedStudent);

            var expectedCourseDTO = A.New<Course_DTO>();
            SetupFetchAsync<GetCourseById, Course_DTO>(expectedCourseDTO);
            
            var expectedCourseScheduledDTO = A.New<StudentCourseScheduled_DTO>();
            SetupFetchAsync<GetStudentCourseScheduled, StudentCourseScheduled_DTO>(expectedCourseScheduledDTO);

            var expectedInstructorDTO = A.New<Instructor_DTO>();
            SetupFetchAsync<GetInstructorById, Instructor_DTO>(expectedInstructorDTO);

            var expectedCourseScheduledResult = expectedCourseScheduledDTO.AsCourseScheduled(expectedCourseDTO, expectedInstructorDTO);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expectedStudent, result.Student);
                Assert.Equivalent(expectedCourseScheduledResult, result.CourseScheduled);
            });
        }
    }
}
