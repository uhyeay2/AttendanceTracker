using AttendanceTracker.Application.RequestHandlers.CourseHandlers;

namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.CourseControllerTests
{
    public class InsertCourseTests : BaseCourseControllerTest
    {
        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task InsertCourse_Given_CourseNameNotProvided_ShouldThrow_ValidationFailedException(string courseName)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertCourse(new(subjectCode: RandomString(), courseName)));
        }

        [Theory]
        [MemberData(nameof(TestCases.NullEmptyAndWhitespaceString), MemberType = typeof(TestCases))]
        public async Task InsertCourse_Given_SubjectCodeNotProvided_ShouldThrow_ValidationFailedException(string subjectCode)
        {
            await Assert.ThrowsAsync<ValidationFailedException>(async () => await _controller.InsertCourse(new(subjectCode, courseName: RandomString())));
        }

        [Fact]
        public async Task InsertCourse_Given_ValidRequest_ShouldReturn_CourseInserted()
        {
            var existingSubject = await SeedAsync(new SeedSubjectRequest());

            var expected = new InsertCourseRequest(existingSubject.SubjectCode, "New Course");

            var result = await _controller.InsertCourse(expected);

            await _controller.DeleteCourse(result.CourseCode);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Contains(expected.SubjectCode, result.CourseCode);

                Assert.Equal(expected.CourseName, result.Name);
            });
        }
    }
}
