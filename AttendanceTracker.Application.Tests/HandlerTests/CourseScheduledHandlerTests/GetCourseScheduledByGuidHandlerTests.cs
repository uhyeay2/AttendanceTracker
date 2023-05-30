using AttendanceTracker.Application.RequestHandlers.CourseScheduledHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.Tests.HandlerTests.CourseScheduledHandlerTests
{
    public class GetCourseScheduledByGuidHandlerTests : HandlerTest
    {
        private readonly GetCourseScheduledByGuidHandler _handler;

        public GetCourseScheduledByGuidHandlerTests() => _handler = new(_mockDataAccess.Object);

        [Fact]
        public async Task GetCourseScheduledByGuid_Given_CourseScheduledNotExisting_ShouldThrow_DoesNotExistException()
        {
            SetupFetchAsync<GetCourseScheduledByGuid, CourseScheduled_DTO>(null!);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _handler.HandleRequestAsync(new()));
        }

        [Fact]
        public async Task GetCourseScheduledByGuid_Given_CourseScheduledExists_ShouldReturn_ExistingCourseScheduled()
        {
            var expectedCourseScheduledDTO = A.New<CourseScheduled_DTO>();
            var expectedInstructor = A.New<Instructor_DTO>();
            var expectedCourse = A.New<Course_DTO>();

            SetupFetchAsync<GetCourseScheduledByGuid, CourseScheduled_DTO>(expectedCourseScheduledDTO);
            SetupFetchAsync<GetInstructorById, Instructor_DTO>(expectedInstructor);
            SetupFetchAsync<GetCourseById, Course_DTO>(expectedCourse);

            var result = await _handler.HandleRequestAsync(new());

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(expectedCourseScheduledDTO.StartDate, result.StartDate);
                Assert.Equal(expectedCourseScheduledDTO.EndDate, result.EndDate);

                Assert.Equal(expectedCourse.CourseCode, result.Course.CourseCode);
                Assert.Equal(expectedCourse.Name, result.Course.Name);

                Assert.Equal(expectedInstructor.InstructorCode, result.Instructor.InstructorCode);
                Assert.Equal(expectedInstructor.FirstName, result.Instructor.FirstName);
                Assert.Equal(expectedInstructor.LastName, result.Instructor.LastName);
            });
        }
    }
}
