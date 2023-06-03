using AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers;

namespace AttendanceTracker.Api.Tests.ControllerIntegrationTests.StudentAttendanceOccurenceControllerTests
{
    public class InsertStudentAttendanceOccurenceTests : BaseStudentAttendanceOccurenceControllerTest
    {
        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_StudentCodeNotExisting_ShouldThrow_DoesNotExistException()
        {
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var request = new InsertStudentAttendanceOccurenceRequest(studentCode: RandomString(), existingCourseScheduled.Guid, DateTime.Now, "Notes", isExcused: true);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.InsertStudentAttendanceOccurence(request));
        }

        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_CourseScheduledGuidNotExisting_ShouldThrow_DoesNotExistException()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            var request = new InsertStudentAttendanceOccurenceRequest(existingStudent.StudentCode, courseScheduledGuid: Guid.NewGuid(), DateTime.Now, "Notes", isExcused: true);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.InsertStudentAttendanceOccurence(request));
        }

        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_StudentAndCourseScheduledGuidExisting_ButStudentNotScheduledForCourse_ShouldThrow_DoesNotExistException()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            var request = new InsertStudentAttendanceOccurenceRequest(existingStudent.StudentCode, existingCourseScheduled.Guid, DateTime.Now, "Notes", isExcused: true);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await _controller.InsertStudentAttendanceOccurence(request));
        }

        [Fact]
        public async Task InsertStudentAttendanceOccurence_Given_StudentScheduledForCourse_ShouldReturn_StudentAttendanceOccurenceInserted()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());
            var existingCourseScheduled = await SeedAsync(new SeedCourseScheduledRequest());

            await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode, existingCourseScheduled.Guid));

            var request = new InsertStudentAttendanceOccurenceRequest(existingStudent.StudentCode, existingCourseScheduled.Guid, DateTime.Today, RandomString(), isExcused: true);

            var result = await _controller.InsertStudentAttendanceOccurence(request);

            await _controller.DeleteStudentAttendanceOccurence(result.AttendanceOccurence.Guid);

            Assert.Multiple(() =>
            {
                Assert.NotNull(result);

                Assert.Equal(request.Notes, result.AttendanceOccurence.Notes);
                Assert.Equal(request.DateOfOccurence, result.AttendanceOccurence.DateOfOccurence);
                Assert.Equal(request.IsExcused, result.AttendanceOccurence.IsExcused);
            });
        }
    }
}
