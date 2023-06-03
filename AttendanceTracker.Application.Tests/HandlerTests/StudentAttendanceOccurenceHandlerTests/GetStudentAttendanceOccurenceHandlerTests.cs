using AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers;
using AttendanceTracker.Application.RequestHandlers.StudentHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Models;

namespace AttendanceTracker.Application.Tests.HandlerTests.StudentAttendanceOccurenceHandlerTests
{
    public class GetStudentAttendanceOccurenceHandlerTests : HandlerTest
    {
        private readonly GetStudentAttendanceOccurenceHandler _handler;

        public GetStudentAttendanceOccurenceHandlerTests() => _handler = new(_mockDataAccess.Object, _mockOrchestrator.Object);

        [Fact]
        public async Task GetStudentAttendanceOccurence_Given_NoCourseScheduledWithStudentCodeAndGuidProvided_ShouldThrow_DoesNotExistException()
        {
            SetupFetchAsync<GetStudentAttendanceOccurence, StudentAttendanceOccurence_DTO>(null!);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task GetStudentAttendanceOccurence_Given_CourseScheduledExists_ShouldReturn_StudentAndCourseScheduledFetchedWithOrchestrator()
        {
            var expectedAttendanceOccurence = A.New<StudentAttendanceOccurence_DTO>();
            SetupFetchAsync<GetStudentAttendanceOccurence, StudentAttendanceOccurence_DTO>(expectedAttendanceOccurence);

            var expectedCourseScheduled = A.New<StudentCourseScheduled_DTO>();
            SetupFetchAsync<GetStudentCourseScheduledById, StudentCourseScheduled_DTO>(expectedCourseScheduled);

            var expectedInstructorDTO = A.New<Instructor_DTO>();
            SetupFetchAsync<GetInstructorById, Instructor_DTO>(expectedInstructorDTO);

            var expectedCourseDTO = A.New<Course_DTO>();
            SetupFetchAsync<GetCourseById, Course_DTO>(expectedCourseDTO);

            var expectedStudent = A.New<Student_DTO>();
            SetupFetchAsync<GetStudentById, Student_DTO>(expectedStudent);

            var expectedResult = expectedAttendanceOccurence.AsStudentAttendanceOccurence(expectedStudent, expectedCourseScheduled, expectedCourseDTO, expectedInstructorDTO);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equivalent(expectedResult, result);
            });
        }
    }
}
