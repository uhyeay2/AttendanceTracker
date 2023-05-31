namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentCourseScheduledControllerTests
{
    public class GetAllStudentCourseScheduledByStudentCodeTests : BaseStudentCourseScheduledControllerTest
    {
        [Fact]
        public async Task GetAllStudentCourseScheduledByStudentCode_Given_NoCoursesFound_ShouldThrow_DoesNotExistsException()
        {
            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.GetAllCoursesScheduledByStudentCode(studentCode: RandomString()));
        }

        [Fact]
        public async Task GetAllStudentCourseScheduledByStudentCode_Given_StudentHasCoursesScheduled_ShouldReturn_StudentWithCoursesScheduled()
        {
            var expectedStudentDTO = await SeedAsync(new SeedStudentRequest());

            var expectedCourseOne = await SeedAsync(new SeedStudentCourseScheduledRequest(studentCode: expectedStudentDTO.StudentCode));
            var expectedCourseTwo = await SeedAsync(new SeedStudentCourseScheduledRequest(studentCode: expectedStudentDTO.StudentCode));
            var expectedCourseThree = await SeedAsync(new SeedStudentCourseScheduledRequest(studentCode: expectedStudentDTO.StudentCode));

            var result = await _controller.GetAllCoursesScheduledByStudentCode(expectedStudentDTO.StudentCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equivalent(expectedStudentDTO.AsStudent(), result.Student);

                Assert.Contains(expectedCourseOne.Guid, result.CoursesScheduled.Select(_ => _.Guid));
                Assert.Contains(expectedCourseTwo.Guid, result.CoursesScheduled.Select(_ => _.Guid));
                Assert.Contains(expectedCourseThree.Guid, result.CoursesScheduled.Select(_ => _.Guid));
            });
        }
    }
}
