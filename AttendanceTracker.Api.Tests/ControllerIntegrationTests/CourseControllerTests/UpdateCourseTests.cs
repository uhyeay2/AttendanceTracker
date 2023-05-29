using AttendanceTracker.Application.RequestHandlers.CourseHandlers;

namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseControllerTests
{
    public class UpdateCourseTests : BaseCourseControllerTest
    {
        [Fact]
        public async Task UpdateCourse_Given_CourseNotExisting_ShouldThrow_DoesNotExistException()
        {
            var updateRequest = new UpdateCourseRequest(courseCode: RandomString());
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.UpdateCourse(updateRequest));
        }

        [Fact]
        public async Task UpdateCourse_Given_CourseIsExisting_ShouldThrow_NoException()
        {
            var existingCourse = await SeedAsync(new SeedCourseRequest());

            var updateRequest = new UpdateCourseRequest(courseCode: existingCourse.CourseCode);

            var exception = await Record.ExceptionAsync(async () => await _controller.UpdateCourse(updateRequest));

            Assert.Null(exception);
        }

        [Fact]
        public async Task UpdateCourse_Given_CourseIsExisting_ShouldUpdate_CourseWithMatchingCourseCode()
        {
            var existingCourse = await SeedAsync(new SeedCourseRequest());

            var expected = new UpdateCourseRequest(existingCourse.CourseCode, name: RandomString());

            await _controller.UpdateCourse(expected);

            var courseAfterUpdate = await _controller.GetCourseByCourseCode(existingCourse.CourseCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(courseAfterUpdate);

                Assert.Equal(existingCourse.CourseCode, courseAfterUpdate.CourseCode);
                Assert.Equal(expected.Name, courseAfterUpdate.Name);
            });
        }
    }
}
