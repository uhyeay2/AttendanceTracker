namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentCourseScheduledControllerTests
{
    public class InsertStudentCourseScheduledTests : BaseStudentCourseScheduledControllerTest
    {
        [Fact]
        public async Task InsertStudentCourseScheduled_Given_StudentNotExisting_ShouldThrow_DoesNotExistException()
        {
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.InsertCourseScheduled(new(studentCode: RandomString(), existingCourseScheduled.Guid)));
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_CourseScheduledGuidNotExisting_ShouldThrow_DoesNotExistException()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.InsertCourseScheduled(new(existingStudent.StudentCode, courseScheduledGuid: Guid.NewGuid())));
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_StudentCourseScheduledAlreadyExists_ShouldThrow_AlreadyExistsException()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());
            var existingStudentCourseScheduled = await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode));

            await Assert.ThrowsAsync<AlreadyExistsException>(async () => await _controller.InsertCourseScheduled(new(existingStudent.StudentCode, existingStudentCourseScheduled.Guid)));
        }

        [Fact]
        public async Task InsertStudentCourseScheduled_Given_StudentCourseScheduledIsInserted_ShouldReturn_StudentCourseScheduled()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var result = await _controller.InsertCourseScheduled(new(existingStudent.StudentCode, existingCourseScheduled.Guid));

            await _controller.DeleteStudentCourseScheduled(new(existingStudent.StudentCode, existingCourseScheduled.Guid));

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(existingStudent.StudentCode, result.Student.StudentCode);
                Assert.Equal(existingStudent.DateOfBirth, result.Student.DateOfBirth);
                Assert.Equal(existingStudent.FirstName, result.Student.FirstName);
                Assert.Equal(existingStudent.LastName, result.Student.LastName);

                Assert.Equal(existingCourseScheduled.StartDate, result.CourseScheduled.StartDate);
                Assert.Equal(existingCourseScheduled.EndDate, result.CourseScheduled.EndDate);
                Assert.Equal(existingCourseScheduled.Guid, result.CourseScheduled.Guid);
            });
        }
    }
}
