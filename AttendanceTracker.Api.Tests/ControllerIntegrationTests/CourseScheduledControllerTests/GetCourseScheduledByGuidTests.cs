namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseScheduledControllerTests
{
    public class GetCourseScheduledByGuidTests : BaseCourseScheduledControllerTest
    {
        [Fact]
        public async Task GetCourseScheduledByGuid_Given_EmptyGuid_ShouldThrow_ValidationFailedException()
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.GetCourseScheduledByGuid(Guid.Empty));
        }

        [Fact]
        public async Task GetCourseScheduledByGuid_Given_CourseScheduledNotExisting_ShouldThrow_DoesNotExistException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.GetCourseScheduledByGuid(Guid.NewGuid()));
        }

        [Fact]
        public async Task GetCourseScheduledByGuid_Given_CourseScheduledIsExisting_ShouldReturn_CourseScheduled()
        {
            var existingCourse = await SeedAsync(new SeedCourseRequest());
            var existingInstructor = await SeedAsync(new SeedInstructorRequest());

            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest(courseCode: existingCourse.CourseCode, instructorCode: existingInstructor.InstructorCode));

            var result = await _controller.GetCourseScheduledByGuid(existingCourseScheduled.Guid);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(existingCourseScheduled.StartDate, result.StartDate);
                Assert.Equal(existingCourseScheduled.EndDate, result.EndDate);
                Assert.Equal(existingCourseScheduled.Guid, result.Guid);

                Assert.Equal(existingCourse.CourseCode, result.Course.CourseCode);
                Assert.Equal(existingCourse.Name, result.Course.Name);

                Assert.Equal(existingInstructor.InstructorCode, result.Instructor.InstructorCode);
                Assert.Equal(existingInstructor.FirstName, result.Instructor.FirstName);
                Assert.Equal(existingInstructor.LastName, result.Instructor.LastName);
            });
        }
    }
}
