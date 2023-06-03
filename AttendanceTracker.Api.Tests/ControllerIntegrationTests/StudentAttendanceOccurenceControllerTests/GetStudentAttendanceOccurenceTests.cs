namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentAttendanceOccurenceControllerTests
{
    public class GetStudentAttendanceOccurenceTests : BaseStudentAttendanceOccurenceControllerTest
    {
        [Fact]
        public async Task GetStudentAttendanceOccurence_Given_OccurenceNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.GetStudentAttendanceOccurence(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetStudentAttendanceOccurence_Given_EmptyGuid_ShouldThrow_ValidationFailedException()
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetStudentAttendanceOccurence(Guid.Empty));
        }

        [Fact]
        public async Task GetStudentAttendanceOccurence_Given_OccurenceExists_ShouldReturn_ExpectedStudentAttendanceOccurence()
        {
            var expectedStudentDto = await SeedAsync(new SeedStudentRequest());
            var expectedInstructorDto = await SeedAsync(new SeedInstructorRequest());
            var expectedCourseDto = await SeedAsync(new SeedCourseRequest());

            var expectedCourseScheduledDto = await SeedAsync(new SeedCourseScheduledRequest(courseCode: expectedCourseDto.CourseCode, instructorCode: expectedInstructorDto.InstructorCode));
            var expectedStudentCourseScheduledDto = await SeedAsync(new SeedStudentCourseScheduledRequest(expectedStudentDto.StudentCode, expectedCourseScheduledDto.Guid));

            var expectedAttendanceOccurence = await SeedAsync(new SeedStudentAttendanceOccurenceRequest(expectedStudentDto.StudentCode, expectedCourseScheduledDto.Guid));

            var result = await _controller.GetStudentAttendanceOccurence(expectedAttendanceOccurence.Guid);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equivalent(expectedStudentDto.AsStudent(), result.Student);
                Assert.Equivalent(expectedStudentCourseScheduledDto.AsCourseScheduled(expectedCourseDto, expectedInstructorDto), result.CourseScheduled);
                Assert.Equivalent(expectedAttendanceOccurence.AsAttendanceOccurence(), result.AttendanceOccurence);
            });
        }
    }
}
