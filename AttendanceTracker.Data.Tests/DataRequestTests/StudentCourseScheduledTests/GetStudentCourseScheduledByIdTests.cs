using AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests;

namespace AttendanceTracker.Data.Tests.DataRequestTests.StudentCourseScheduledTests
{
    public class GetStudentCourseScheduledByIdTests : DataTest
    {
        [Fact]
        public async Task GetStudentCourseScheduledById_Given_CourseScheduledNotExisting_ShouldReturn_Null()
        {
            var existingStudent = await SeedAsync(new SeedStudentRequest());

            // insert a different course for this student to ensure we don't accidentally get the other course
            await SeedAsync(new SeedStudentCourseScheduledRequest(existingStudent.StudentCode));

            var result = await _dataAccess.FetchAsync(new GetStudentCourseScheduledById(int.MinValue));

            Assert.Null(result);
        }

        [Fact]
        public async Task GetStudentCourseScheduledById_Given_CourseScheduledIsExisting_ShouldReturn_CourseScheduled()
        {
            // insert a different course for this student to ensure we don't accidentally get the other course
            var existingStudentCourseScheduled = await SeedAsync(new SeedStudentCourseScheduledRequest());

            var result = await _dataAccess.FetchAsync(new GetStudentCourseScheduledById(existingStudentCourseScheduled.Id));

            Assert.NotNull(result);
        }
    }
}
