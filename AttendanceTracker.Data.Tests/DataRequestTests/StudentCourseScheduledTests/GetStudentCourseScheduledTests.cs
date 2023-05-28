using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentCourseScheduledTests
{
    public class GetStudentCourseScheduledTests : DataTest
    {
        [Fact]
        public async Task GetStudentCourseScheduled_Given_CourseScheduledNotExisting_ShouldReturn_Null()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            // insert a different course for this student to ensure we don't accidentally get the other course
            await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode));

            var result = await _dataAccess.FetchAsync(new GetStudentCourseScheduled(existingStudent.StudentCode, Guid.NewGuid()));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetStudentCourseScheduled_Given_CourseScheduledIsExisting_ShouldReturn_CourseScheduled()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            // insert a different course for this student to ensure we don't accidentally get the other course
            var existingStudentCourseScheduled = await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode));

            var result = await _dataAccess.FetchAsync(new GetStudentCourseScheduled(existingStudent.StudentCode, existingStudentCourseScheduled.Guid));

            Assert.NotNull(result);
        }
    }
}
